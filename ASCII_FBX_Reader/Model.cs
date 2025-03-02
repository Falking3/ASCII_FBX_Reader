using System.Numerics;

public class Model
{
    public List<Face> faces = new List<Face>();
    string modelName = null; //needs implemented properly
    //tri count
    //vert count
    //various flags for things being wrong

    public Model()
    {
    }
    public Model(List<Face> faces, string modelName)
    {
        this.faces = faces;
        this.modelName = modelName;
    }

    public void Print()
    {
        foreach (Face face in faces)
        {
            face.Print();
        }

    }
}
