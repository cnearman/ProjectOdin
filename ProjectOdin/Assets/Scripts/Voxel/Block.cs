using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class Block 
{
    public int blockInt;

    public bool air;

	public bool Modified { get; set; }

    public struct Tile { public int x; public int y; }

    const float tileSize = 0.25f;

    public Block()
	{
        this.blockInt = 1;

		this.Modified = true;
        this.air = false;
	}

    public virtual Vector2[] FaceUVs(Direction direction)
    {
        Vector2[] UVs = new Vector2[4];
        Tile tilePos = TexturePosition(direction);

        UVs[0] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y);
        UVs[1] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y + tileSize);
        UVs[2] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y + tileSize);
        UVs[3] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y);

        return UVs;
    }

    public virtual Tile TexturePosition(Direction direction)
	{
		Tile tile = new Tile ();
		tile.x = 0;
		tile.y = 0;
		return tile;
	}

	public virtual bool IsSolid(Direction direction)
	{

		switch (direction.Id) 
		{
		case 0:
			return true;
		case 1:
			return true;
		case 2:
			return true;
		case 3:
			return true;
		case 4:
			return true;
		case 5:
			return true;
		}

		return false;
	}
	
	protected virtual MeshData FaceData(int x, int y, int z, Direction dir, MeshData meshData)
	{
		meshData.AddVertex(new Vector3(x + dir.Vertices[0,0] * 0.5f,y + dir.Vertices[0,1] * 0.5f, z + dir.Vertices[0,2] * 0.5f));
		meshData.AddVertex(new Vector3(x + dir.Vertices[1,0] * 0.5f,y + dir.Vertices[1,1] * 0.5f, z + dir.Vertices[1,2] * 0.5f));
		meshData.AddVertex(new Vector3(x + dir.Vertices[2,0] * 0.5f,y + dir.Vertices[2,1] * 0.5f, z + dir.Vertices[2,2] * 0.5f));
		meshData.AddVertex(new Vector3(x + dir.Vertices[3,0] * 0.5f,y + dir.Vertices[3,1] * 0.5f, z + dir.Vertices[3,2] * 0.5f));

		meshData.AddQuadTriangles ();

		meshData.uv.AddRange(FaceUVs(dir));

		return meshData;
	}

	public virtual MeshData FaceDataUp(int x, int y, int z, MeshData meshData)
	{
		return FaceData (x, y, z, Direction.Up, meshData);
	}

	public virtual MeshData FaceDataDown(int x, int y, int z, MeshData meshData)
	{
		return FaceData(x, y, z, Direction.Down, meshData);
	}

    public virtual MeshData FaceDataNorth(int x, int y, int z, MeshData meshData)
    {
        return FaceData(x, y, z, Direction.North, meshData);
    }

    public virtual MeshData FaceDataSouth(int x, int y, int z, MeshData meshData)
	{
		return FaceData(x, y, z, Direction.South, meshData);
	}

	public virtual MeshData FaceDataLeft(int x, int y, int z, MeshData meshData)
	{
		return FaceData(x, y, z, Direction.Left, meshData);
	}

	public virtual MeshData FaceDataRight(int x, int y, int z, MeshData meshData)
	{
		return FaceData(x, y, z, Direction.Right, meshData);
	}
}