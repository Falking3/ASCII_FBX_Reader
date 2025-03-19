using System.Text.RegularExpressions;
using System.IO;
using ASCII_FBX_Reader;


//Application app = new App();

Console.WriteLine("Hello, World!");
var filepath = @"C:\\Users\\dunca\\Documents\\GitHub\\ASCII_FBX_Reader\\ascii_test_mesh_01.fbx";
//@"D:\\GitHub\\ASCII_FBX_Reader\\ascii_test_mesh_01.fbx";  
//WPF file selector
Model model = ModelRead.Read(filepath);

//WPF display for the output
model.Print();

ValidateModel.Validate(model);

