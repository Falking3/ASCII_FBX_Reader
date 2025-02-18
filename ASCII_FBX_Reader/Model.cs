using System.Numerics;

public class Model
{
    public List<Face> faces = new List<Face>();
    string modelName = null;

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
