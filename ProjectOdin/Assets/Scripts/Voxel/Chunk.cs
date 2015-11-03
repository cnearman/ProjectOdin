using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{
    public Block[,,] blocks = new Block[chunkSize, chunkSize, chunkSize];

    public static int chunkSize = 32;
    public bool update = false;

    MeshFilter filter;
    MeshCollider coll;

    public World world;
    public WorldPos pos;

    public bool rendered;


    // Use this for initialization
    void Start()
    {

        filter = gameObject.GetComponent<MeshFilter>();
        coll = gameObject.GetComponent<MeshCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            update = false;
            UpdateChunk();
        }
    }

    public void SetBlocksUnmodified()
    {
        foreach (Block block in blocks)
        {
            block.Modified = false;
        }
    }

    public Block GetBlock(int x, int y, int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
            return blocks[x, y, z];
        return world.GetBlock(pos.x + x, pos.y + y, pos.z + z);
    }

    //new function
    public static bool InRange(int index)
    {
        if (index < 0 || index >= chunkSize)
            return false;

        return true;
    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            blocks[x, y, z] = block;
        }
        else
        {
            world.SetBlock(pos.x + x, pos.y + y, pos.z + z, block);
        }
    }

    void UpdateChunk()
    {

        rendered = true;

        MeshData meshData = new MeshData();

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    meshData = BlockData(x, y, z, meshData);
                    //meshData = blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                }
            }
        }

        RenderMesh(meshData);
    }

    MeshData BlockData(int x, int y, int z, MeshData meshData)
    {
        meshData.useRenderDataForCol = true;

        if (!GetBlock(x, y + 1, z).IsSolid(Direction.Down))
        {
            meshData =  FaceDataUp(x, y, z, meshData);
        }

        if (!GetBlock(x, y - 1, z).IsSolid(Direction.Up))
        {
            meshData =  FaceDataDown(x, y, z, meshData);
        }

        if (!GetBlock(x, y, z + 1).IsSolid(Direction.South))
        {
            meshData = FaceDataNorth(x, y, z, meshData);
        }

        if (!GetBlock(x, y, z - 1).IsSolid(Direction.North))
        {
            meshData = FaceDataSouth(x, y, z, meshData);
        }

        if (!GetBlock(x + 1, y, z).IsSolid(Direction.Left))
        {
            meshData = FaceDataLeft(x, y, z, meshData);
        }

        if (!GetBlock(x - 1, y, z).IsSolid(Direction.Right))
        {
            meshData = FaceDataRight(x, y, z, meshData);
        }

        return meshData;
    }

    // Sends the calculated mesh information
    // to the mesh and collision components
    void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();
        filter.mesh.uv = meshData.uv.ToArray();

        filter.mesh.RecalculateNormals();

        //additions:
        coll.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();
        mesh.RecalculateNormals();

        coll.sharedMesh = mesh;
    }

}
