using System.Numerics;
using System.Windows.Controls;

public class Face
{
    public List<Vertex> verts = new List<Vertex>();
    public List<Vector3> normals = new List<Vector3>();
    public List<int> polyvert_IDs = new List<int>();
    public List<Vector2> uvs = new List<Vector2>();
    public int FaceID = 0;
    bool tri = false;
    bool quad = false;
    bool ngon = false;

    public Face()
    {
    }
    public override bool Equals(object obj)
    {
        if (!(obj is Face temp))
        {
            return false;
        }
        return (verts.Count == temp.verts.Count) && !verts.Except(temp.verts).Any();

    }
    public Face(Face oldface)
    {
        verts = new List<Vertex>(oldface.verts);
        normals = new List<Vector3>(oldface.normals);
        polyvert_IDs = new List<int>(oldface.polyvert_IDs);
        uvs = new List<Vector2>(oldface.uvs);
        FaceID = oldface.FaceID;
        tri = oldface.tri;
        quad = oldface.quad;
        ngon = oldface.ngon;
    }
    public Face(List<Vertex> Verts, List<int>polyvert_id_list, int ID)
    {
        foreach(Vertex vert in verts)
        {
            Vertex newvert = new Vertex();
            newvert.VertexID = vert.VertexID;
            newvert.Coords = vert.Coords;
            Verts.Add(newvert);
        }
        verts = Verts;  //new List<Vertex>(verts);
        polyvert_IDs = new List<int>(polyvert_id_list);
        if (Verts.Count == 3)
        {
            tri = true;
        }
        else if (Verts.Count == 4)
        { 
            quad = true;
        }
        else
        {
            ngon = true;
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
        if (tri == true)
        {
            Console.WriteLine("Is a tri");
        }
        else if (quad == true)
        {
            Console.WriteLine("Is a quad");
        }
        else
        {
            Console.WriteLine("Is an ngon");
        }
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
