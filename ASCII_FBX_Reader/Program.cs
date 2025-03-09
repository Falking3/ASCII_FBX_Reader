using System.Text.RegularExpressions;
using System.IO;


//Application app = new App();

Console.WriteLine("Hello, World!");
var filepath = @"D:\\GitHub\\ASCII_FBX_Reader\\ascii_test_mesh_01.fbx";  //"C:\\Users\\dunca\\Documents\\GitHub\\ASCII_FBX_Reader\\ascii_test_mesh_01.fbx"

//WPF file selector
Model model = ModelRead.Read(filepath);

//WPF display for the output
model.Print();


for (int i = 1; i < model.faces.Count; i++)

{
    Face face1 = model.faces[i];
    int j = i -1 ;
    Face face2 = model.faces[j];
    Console.WriteLine($"{face1.FaceID} + {face2.FaceID}");
    Console.WriteLine(face1.Equals(face2));

}
