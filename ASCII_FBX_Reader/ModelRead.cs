using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using System.Windows.Media;

public static class ModelRead
{
    const float diverging_normal_angle = 90f;

	public static Model Read(string path) //populates a new model variable with the data from the specified file
	{

        //Dump the entire FBX file to a string
        string ReadContents;
        using (StreamReader streamReader = new StreamReader(path))
        {
            ReadContents = streamReader.ReadToEnd();
        }


        //Regex match for {} after "Vertices"
        List<float> coords_list = new List<float>();
        coords_list = ParseFBXValues.Parse(@"\bVertices\b.+\n\s+a:([^}]+\n)", ReadContents, coords_list);

        //Regex match for {} after "UVIndex"
        List<int> uvindex_list = new List<int>();
        uvindex_list = ParseFBXValues.Parse(@"\bUVIndex\b.+\n\s+a:([^}]+\n)", ReadContents, uvindex_list);

        //Regex match for {} after "PolygonVertexIndex"
        List<int>face_index_list = new List<int>();
        face_index_list = ParseFBXValues.Parse(@"\bPolygonVertexIndex\b.+\n\s+a:([^}]+\n)", ReadContents, face_index_list);


        //Regex match for {} after "Normals"
        List<float> normals_list = new List<float>();
        normals_list = ParseFBXValues.Parse(@"\bNormals\b.+\n\s+a:([^}]+\n)", ReadContents, normals_list);

        //Regex match for {} after "NormalsIndex"
        List<int> normal_index_list = new List<int>();
        normal_index_list = ParseFBXValues.Parse(@"\bNormalsIndex\b.+\n\s+a:([^}]+\n)", ReadContents, normal_index_list);


        //Regex match for {} after "UV"
        List<Vector2> uv_list = new List<Vector2>();
        uv_list = ParseFBXValues.Parse(@"\bUV\b.+\n\s+a:([^}]+\n)", ReadContents, uv_list);

        //Regex match for "Material:: (materialname)"
        List<string> material_list = new List<string>();
        material_list = ParseFBXValues.Parse(@"""Material::([^""]+)""", ReadContents, material_list);


        //Set up component lists/dicts
        List<Vertex> vert_array = new List<Vertex>(); 
        List<Face> faceslist = new List<Face>();
        Dictionary<int, Vertex> Vert_To_ID = new Dictionary<int, Vertex>(); //do we use this?


        //fill out vertices --------------------------------------------------

        int index = -1; //can we tie this into the loop iterator at all?
        for (int i = 0; i < coords_list.Count - 2; i += 3)
        {
            index++; //verts are groups of 3 coords, this is the index of the vertex
            Vertex vertex = new Vertex(coords_list[i], coords_list[i + 1], coords_list[i + 2], index);
            Vert_To_ID[index] = vertex;
            vert_array.Add(vertex);
        }

        ////////////////////////////////////

        //fill out faces
        //

        int faceindex = 0;
        bool moveToNewFace = false;
        int face_array_index = 0;
        List<Vertex>vert_list = new List<Vertex>();
        List<int> polyvert_id_list = new List<int>();
        for (int i = 0; i < face_index_list.Count; i++)
        {
       

        if (i == face_index_list.Count - 1)
        {
                vert_list.Add(Vert_To_ID[(face_index_list[i] * -1 - 1)]); //XOR with -1 to get actual vertex index
                polyvert_id_list.Add(i);
                moveToNewFace = true;
        }


        if (moveToNewFace == true) //not sure if there's a more elegant solution considering faces have a variable number of vertices
            {
                Face curface = new Face(vert_list, polyvert_id_list, face_array_index);

                Console.WriteLine("New face");
                for (int j = 0; j < vert_list.Count(); j++)
                {
                    curface.normals.Add(new Vector3(0, 0, 0));
                    curface.uvs.Add(new Vector2(0, 0));
                }
                faceslist.Add(new Face(curface));

                face_array_index++;
                vert_list.Clear();
                polyvert_id_list.Clear();
                moveToNewFace = false;
            }

            if (face_index_list[i] < 0) //a negative value means it's the end of a face
            {
                moveToNewFace = true;
                vert_list.Add(Vert_To_ID[(face_index_list[i] * -1 - 1)]); //XOR with -1 to get actual vertex index
                polyvert_id_list.Add(i);

            }
            else
            {
                vert_list.Add(Vert_To_ID[face_index_list[i]]);
                polyvert_id_list.Add(i);



            }
        }

        List<Vector3> grouped_normals_list = new List<Vector3>();
        for (int i = 0; i < normals_list.Count; i++)
        {   
            Vector3 normal_index = new Vector3(normals_list[i], normals_list[++i], normals_list[++i]);
            grouped_normals_list.Add(normal_index);
        }

        /////////////////////////////////////////////////
        ///

        //The VALUE of NormalsIndex relates to the position of the normal in the "Normals" array.
        //The polygon vertices that the normals belong to are the order listed in "PolygonVertexIndex"


        for (int i = 0; i < normal_index_list.Count; ++i)
        {
        Face currface = faceslist[0];
        int list_index = 0;
        int n_index = normal_index_list[i];

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

       

        //////////////////////////////////////////////////



        for ( int i = 0;i < uvindex_list.Count; i++)
        {
            int uv_index = uvindex_list[i];
            Vector2 uv_value = uv_list[uv_index];

        foreach(Face face in faceslist)
            {
                face.CheckDivergingNormals(diverging_normal_angle);
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

        
        return new Model(faceslist, "Model", material_list);
    }
}

