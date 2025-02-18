using System.Numerics;

public class Face
{
    public List<Vertex> verts = new List<Vertex>();
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

    }
}
