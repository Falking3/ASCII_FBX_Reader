﻿using System.Numerics;

public class Vertex
{
	Vector3 Coords = new Vector3(0,0,0);
    public Vector3 Normal = new Vector3(0, 0, 0);
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
	public void Print()
	{
		Console.WriteLine($"Vertex ID: {VertexID}, Coords: {Coords.ToString()}, Normals: {Normal}");
	}
}
