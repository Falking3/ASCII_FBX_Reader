using System.Text.RegularExpressions;
using System.IO;

Console.WriteLine("Hello, World!");
var path = @"D:\\GitHub\\ASCII_FBX_Reader\\ascii_test_mesh_01.fbx";  //"C:\\Users\\dunca\\Documents\\GitHub\\ASCII_FBX_Reader\\ascii_test_mesh_01.fbx"

string ReadContents;
using (StreamReader streamReader = new StreamReader(path))
{
    ReadContents = streamReader.ReadToEnd();
}

string regexpattern = @"\bVertices\b.+\n\s+a:([^}]+\n)";
Regex rg = new Regex(regexpattern);

MatchCollection matches = rg.Matches(ReadContents);
string verts = matches[0].Groups[1].Value.ToString();
float[] coords_array = Array.ConvertAll(verts.Split(","), float.Parse);

string regexpattern_faces = @"\bPolygonVertexIndex\b.+\n\s+a:([^}]+\n)";
Regex rg_faces = new Regex(regexpattern_faces);
MatchCollection face_matches = rg_faces.Matches(ReadContents);
string faces = face_matches[0].Groups[1].Value.ToString();
int[] face_index_array = Array.ConvertAll(faces.Split(","), int.Parse);
Vertex[] vert_array = new Vertex[coords_array.Length / 3];
List<Face> faceslist = new List<Face>();
Dictionary<int, Vertex> Vert_To_ID = new Dictionary<int, Vertex>();

int index = -1;
for (int i = 0; i < coords_array.Length - 2; i += 3)
{
    index++;
    Vertex vertex = new Vertex(coords_array[i], coords_array[i + 1], coords_array[i + 2], index);
    Vert_To_ID[index] = vertex;
    vert_array.Append(vertex);
}

int faceindex = 0;
bool moveToNewFace = false;
Face curface = new Face(0);
faceslist.Add(curface);
int face_array_index = 0;
for (int i = 0; i < face_index_array.Length; i++)
{
    if (moveToNewFace == true)
    {
        face_array_index++;
        moveToNewFace = false;
        curface = new Face(face_array_index);
        faceslist.Add(curface);
    }
    if (face_index_array[i] < 0)
    {
        moveToNewFace = true;
        curface.verts.Add(Vert_To_ID[(face_index_array[i] *-1 -1)]);
    }
    else
    {
        curface.verts.Add(Vert_To_ID[face_index_array[i]]);
    }

}


Model model = new Model(faceslist, "Model");
model.Print();