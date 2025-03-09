using System.Numerics;

public class Vertex
{
	public Vector3 Coords = new Vector3(0,0,0);
    //public Vector3 Normal = new Vector3(0, 0, 0); //this is really stored on face corners, wh
    Vector3 Colour = new Vector3(0, 0, 0);
	List<Vector2> UVCoords = new List<Vector2>();	

    public int VertexID = 0;
	public Vertex()
	{
	}
	public Vertex(float x_coord, float y_coord, float z_coord, int ID)
	{
		Coords = new Vector3(x_coord, y_coord, z_coord);
		VertexID = ID;
	}
	public Vertex(Vertex previous_vert)
	{
		Coords = previous_vert.Coords;
		VertexID = previous_vert.VertexID;
	}
	public void Print()
	{
		Console.WriteLine($"Vertex ID: {VertexID}, Coords: {Coords.ToString()}");
	}
    public override bool Equals(object obj)
    {
		if (!(obj is Vertex temp))
		{
			return false;
		}
        if (this.VertexID == temp.VertexID)
		{
			return true;
		}
		else {  return false; }
    }
}
