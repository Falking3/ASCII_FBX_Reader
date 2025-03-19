using System.Numerics;

public class Model
{
    public List<Face> faces = new List<Face>();
    string modelName = null; //needs implemented properly
    public List<string> materials { get; } = new List<string>();
    //tri count
    //vert count
    //various flags for things being wrong

    public Model()
    {
    }
    public Model(List<Face> faces, string modelName, List<string> material_list)
    {
        this.faces = faces;
        this.modelName = modelName;
        this.materials = material_list;
    }

    public void Print()
    {
        if (materials != null)
        {
            Console.WriteLine("This model has {0} material(s). They are:", materials.Count);
            foreach (string material in materials)
            {
                Console.WriteLine($"- '{material} '");
            }
        }
        else
        {
            Console.WriteLine("This model has no exported materials");
        }

        foreach (Face face in faces)
        {
            face.Print();
        }

    }
}
