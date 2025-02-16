using System.Numerics;

public class Vertex
{
	Vector3 Coords = new Vector3(0,0,0);
    Vector3 Normal = new Vector3(0, 0, 0);
    Vector3 Colour = new Vector3(0, 0, 0);
	Vector2 UVCoords = new Vector2(0,0);	

    int VertexID = 0;
	public Vertex()
	{
	}
	public Vertex(float x_coord, float y_coord, float z_coord, int ID)
	{
		Coords = new Vector3(x_coord, y_coord, z_coord);
		VertexID = ID;
	}
	public void Print()
	{
		Console.WriteLine($"Vertex ID: {VertexID}, Coords: {Coords.ToString()}");
	}
}
