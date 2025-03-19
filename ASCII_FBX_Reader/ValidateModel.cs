using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ASCII_FBX_Reader
{
    internal class ValidateModel
    {
        public static void Validate(Model model) 
        {
            List<Face> faces = new List<Face>();
            foreach (Face face in model.faces)
            {
                if (faces.Contains(face)== true){
                    Console.WriteLine($"Face ID:{face.FaceID} is a duplicate");
                }
                faces.Add(face);
            }
        }

    }
}
