using System.Numerics;

public class Face
{
    public List<Vertex> verts = new List<Vertex>();
    public List<Vector3> normals = new List<Vector3>();
    public List<int> polyvert_IDs = new List<int>();
    public List<Vector2> uvs = new List<Vector2>();
    int FaceID = 0;
    bool tri = false;
    bool quad = false;
    bool ngon = false;

    public Face()
    {
    }
    public Face(List<Vertex> Verts, int ID)
    {
        Verts = verts;
        if (verts[3] == null)
        {
            tri = true;
        }
        else 
        { 
            quad = true;
        }
        FaceID = ID;
    }
    public Face(int ID)
    {
        FaceID = ID;
    }
    public void Print()
    {
        Console.WriteLine($"Face ID: {FaceID}");
        foreach (Vertex vert in verts)
        {
            vert.Print();
        }
        Console.Write("Normals: ");
        foreach (Vector3 v in normals)
        {
            Console.Write($"{v}");
        }

        Console.WriteLine();
        Console.Write("Polyvert IDs:");
        foreach (int i in polyvert_IDs)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine();
        Console.Write("UVs: ");
        foreach (Vector2 vector2 in uvs) 
        {
            Console.Write($"{vector2}");
        }
        Console.WriteLine();
        Console.WriteLine("////////////////////");
    }
}
