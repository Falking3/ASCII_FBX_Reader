using System.Numerics;

public class Face
{
    Vertex[] Verts = new Vertex[4];
    int FaceID = 0;
    bool tri = false;
    bool quad = false;
    bool ngon = false;

    public Face()
    {
    }
    public Face(Vertex[]verts, int ID)
    {
        Verts = verts;
        if (verts[3] == 0)
        {
            tri = true;
        }
        else 
        { 
            quad = true;
        }
        FaceID = ID;
    }
    public void Print()
    {
        Console.WriteLine($"Face ID: {FaceID}, Verts: {Verts.ToString()}");
    }
}
