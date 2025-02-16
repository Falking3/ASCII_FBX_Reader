using System.Numerics;

public class Vertex
{
	Vector3 Coords = new Vector3(0,0,0);
	int VertexID = 0;
	public Vertex()
	{
	}
	public Vertex(int x_coord, int y_coord, int z_coord, int ID)
	{
		Coords = new Vector3(x_coord, y_coord, z_coord);
		VertexID = ID;
	}
	public void Print()
	{
		Console.WriteLine($"Vertex ID: {VertexID}, Coords: {Coords.ToString()}");
	}
}
