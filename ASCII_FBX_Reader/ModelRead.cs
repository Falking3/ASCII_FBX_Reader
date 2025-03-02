using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using System.Windows.Media;

public static class ModelRead
{

	public static Model Read(string path) //populates a new model variable with the data from the specified file
	{

        //Dump the entire FBX file to a string
        string ReadContents;
        using (StreamReader streamReader = new StreamReader(path))
        {
            ReadContents = streamReader.ReadToEnd();
        }

        //Regex match for {} after "Vertices"
        string regexpattern = @"\bVertices\b.+\n\s+a:([^}]+\n)";
        Regex rg = new Regex(regexpattern);
        MatchCollection matches = rg.Matches(ReadContents);
        string verts = matches[0].Groups[1].Value.ToString(); //This is the first real matching group
        float[] coords_array = Array.ConvertAll(verts.Split(","), float.Parse); //Splits a string into an array of floats based on commas

        //Regex match for {} after "PolygonVertexIndex"
        string regexpattern_faces = @"\bPolygonVertexIndex\b.+\n\s+a:([^}]+\n)";
        Regex rg_faces = new Regex(regexpattern_faces);
        MatchCollection face_matches = rg_faces.Matches(ReadContents);
        string faces = face_matches[0].Groups[1].Value.ToString();
        int[] face_index_array = Array.ConvertAll(faces.Split(","), int.Parse);

        //Set up lists
        List<Vertex> vert_array = new List<Vertex>(); 
        List<Face> faceslist = new List<Face>();
        Dictionary<int, Vertex> Vert_To_ID = new Dictionary<int, Vertex>(); //do we use this?


        //fill out vertices
        //

        int index = -1; //can we tie this into the loop iterator at all?
        for (int i = 0; i < coords_array.Length - 2; i += 3)
        {
            index++; //verts are groups of 3 coords, this is the index of the vertex
            Vertex vertex = new Vertex(coords_array[i], coords_array[i + 1], coords_array[i + 2], index);
            Vert_To_ID[index] = vertex;
            vert_array.Add(vertex);
        }

        ////////////////////////////////////

        //fill out faces
        //

        int faceindex = 0;
        bool moveToNewFace = false;
        Face curface = new Face(0);
        faceslist.Add(curface);
        int face_array_index = 0;
        for (int i = 0; i < face_index_array.Length; i++)
        {
            if (moveToNewFace == true) //not sure if there's a more elegant solution considering faces have a variable number of vertices
            {
                face_array_index++;
                moveToNewFace = false;
                curface = new Face(face_array_index);
                faceslist.Add(curface);
            }
            if (face_index_array[i] < 0) //a negative value means it's the end of a face
            {
                moveToNewFace = true;
                curface.verts.Add(Vert_To_ID[(face_index_array[i] * -1 - 1)]); //XOR with -1 to get actual vertex index
                curface.normals.Add(new Vector3(0, 0, 0));
                curface.polyvert_IDs.Add(i);
                curface.uvs.Add(new Vector2(0, 0));
                
            }
            else
            {
                curface.verts.Add(Vert_To_ID[face_index_array[i]]);
                curface.normals.Add(new Vector3(0, 0, 0));
                curface.polyvert_IDs.Add(i);
                curface.uvs.Add(new Vector2(0, 0));

            }

        }

        //////////////////////////////////////


        //Regex match for {} after "Normals"
        string regexpattern_normals = @"\bNormals\b.+\n\s+a:([^}]+\n)";
        Regex rg_normals = new Regex(regexpattern_normals);
        MatchCollection normal_matches = rg_normals.Matches(ReadContents);
        string normals = normal_matches[0].Groups[1].Value.ToString();
        float[] normal_array = Array.ConvertAll(normals.Split(","), float.Parse);

        List<Vector3> grouped_normals_list = new List<Vector3>();
        for (int i = 0; i < normal_array.Length; i++)
        {   
            Vector3 normal_index = new Vector3(normal_array[i], normal_array[++i], normal_array[++i]);
            grouped_normals_list.Add(normal_index);
        }

        /////////////////////////////////////////////////
        ///

        //The VALUE of NormalsIndex relates to the position of the normal in the "Normals" array.
        //The polygon vertices that the normals belong to are the order listed in "PolygonVertexIndex"

        //Regex match for {} after "NormalsIndex"
        string regexpattern_normals_index = @"\bNormalsIndex\b.+\n\s+a:([^}]+\n)";
        Regex rg_normals_index = new Regex(regexpattern_normals_index);
        MatchCollection normal_index_matches = rg_normals_index.Matches(ReadContents);
        string normals_index = normal_index_matches[0].Groups[1].Value.ToString();
        int[] normal_index_array = Array.ConvertAll(normals_index.Split(","), int.Parse);

        for (int i = 0; i < normal_index_array.Length; ++i)
        {
        Face currface = faceslist[0];
        int list_index = 0;
        int n_index = normal_index_array[i];

        foreach(Face face in faceslist)
        {
            foreach(int polyvert_id in face.polyvert_IDs)
            {
                if (polyvert_id == n_index)
                {
                    list_index = face.polyvert_IDs.IndexOf(polyvert_id);
                    currface = face;
                }
            }
        }
            currface.normals[list_index] = grouped_normals_list[n_index];
        }

        


        //Regex match for {} after "UV"
        string regexpattern_uv = @"\bUV\b.+\n\s+a:([^}]+\n)";
        Regex rg_uv = new Regex(regexpattern_uv);
        MatchCollection uv_matches = rg_uv.Matches(ReadContents);
        string uv = uv_matches[0].Groups[1].Value.ToString();
        float[] uv_array = Array.ConvertAll(uv.Split(","), float.Parse);
        List<Vector2> uv_list = new List<Vector2>();

        for (int i = 0; i < uv_array.Length; i++)
        {
            Vector2 uv_vec = new Vector2(uv_array[i], uv_array[++i]);
            uv_list.Add(uv_vec);
        }

        //////////////////////////////////////////////////



        //Regex match for {} after "UVIndex"
        string regexpattern_uvindex = @"\bUVIndex\b.+\n\s+a:([^}]+\n)";
        Regex rg_uvindex = new Regex(regexpattern_uvindex);
        MatchCollection uvindex_matches = rg_uvindex.Matches(ReadContents);
        string uvindex = uvindex_matches[0].Groups[1].Value.ToString();
        int[] uvindex_array = Array.ConvertAll(uvindex.Split(","), int.Parse);
        List<Vector2> uvindex_list = new List<Vector2>();

        for ( int i = 0;i < uvindex_array.Length; i++)
        {
            int uv_index = uvindex_array[i];
            Vector2 uv_value = uv_list[uv_index];

        foreach(Face face in faceslist)
            {
                foreach(int ID in face.polyvert_IDs)
                {
                    if (ID == i)
                    {
                        int f_index = face.polyvert_IDs.IndexOf(ID);
                        face.uvs[f_index] = uv_value;
                    }
                }
            }
        }
        return new Model(faceslist, "Model");
    }
}

