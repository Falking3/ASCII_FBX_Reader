using System.Text.RegularExpressions;
using System.IO;

Console.WriteLine("Hello, World!");
var path = @"D:\\GitHub\\ASCII_FBX_Reader\\ascii_test_mesh_01.fbx";  //"C:\\Users\\dunca\\Documents\\GitHub\\ASCII_FBX_Reader\\ascii_test_mesh_01.fbx"
string ReadContents;
using (StreamReader streamReader = new StreamReader(path))
{
    ReadContents = streamReader.ReadToEnd();
}

string regexpattern = @"\b[Vertices].+\n\s+a:([^}]+\n)";
Regex rg = new Regex(regexpattern);

MatchCollection matches = rg.Matches(ReadContents);
//Console.WriteLine(matches.ToString());
string verts = matches[0].Groups[1].Value.ToString();
Console.WriteLine(verts);

int[] coords_array = Array.ConvertAll(verts.Split(","), int.Parse);
Vertex[] vert_array = new Vertex[coords_array.Length / 3];

int index = -1;
for (int i = 0; i < coords_array.Length - 2; i += 3)
{

    index++;
    Vertex vertex = new Vertex(coords_array[i], coords_array[i + 1], coords_array[i + 2], index);
    vert_array.Append(vertex);
    vertex.Print();

}

