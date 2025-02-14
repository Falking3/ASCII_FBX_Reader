// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");
var path = @"C:\\Users\\DuncanNK\\Documents\\GitHub\\ASCII_FBX_Reader\\ascii_test_mesh_01.fbx";
string ReadContents;
using (StreamReader streamReader = new StreamReader(path))
{
    ReadContents = streamReader.ReadToEnd();
}

string regexpattern = @"\b[Vertices].+\n\s+a:([^}]+\n)";
Regex rg = new Regex(regexpattern, RegexOptions.IgnoreCase);

MatchCollection matches = rg.Matches(ReadContents);
//Console.WriteLine(matches.ToString());
for (int count = 0; count < matches.Count; count++)
{
    Console.WriteLine(matches[count].ToString());
}

//string TestString = "FBX_VERSION = 2020";
//Console.WriteLine(ReadContents);
