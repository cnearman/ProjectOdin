using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshLabWorld : MonoBehaviour
{

    public string worldName = "meshworld";

    public Dictionary<WorldPos, MeshLabChunk> chunks = new Dictionary<WorldPos, MeshLabChunk>();
    public GameObject chunkPrefab;

    public void CreateChunk(int x, int y, int z)
    {
        //the coordinates of this chunk in the world
        WorldPos worldPos = new WorldPos(x, y, z);

        //Instantiate the chunk at the coordinates using the chunk prefab
        GameObject newChunkObject = Instantiate(
                        chunkPrefab, new Vector3(worldPos.x, worldPos.y, worldPos.z),
                        Quaternion.Euler(Vector3.zero)
                    ) as GameObject;

        //Get the object's chunk component
        MeshLabChunk newChunk = newChunkObject.GetComponent<MeshLabChunk>();

        //Assign its values
        newChunk.pos = worldPos;
        newChunk.world = this;

        //Add it to the chunks dictionary with the position as the key
        chunks.Add(worldPos, newChunk);



        for (int xi = 0; xi < MeshLabChunk.chunkSize; xi++)
        {
            for (int yi = 0; yi < MeshLabChunk.chunkSize; yi++)
            {
                for (int zi = 0; zi < MeshLabChunk.chunkSize; zi++)
                {
                   
                        SetBlock(x + xi, y + yi, z + zi, new BlockAir());
                   
                       
                }
            }
        }

        newChunk.SetBlocksUnmodified();
    }

   

    public MeshLabChunk GetChunk(int x, int y, int z)
    {
        WorldPos pos = new WorldPos();
        float multiple = MeshLabChunk.chunkSize;
        pos.x = Mathf.FloorToInt(x / multiple) * MeshLabChunk.chunkSize;
        pos.y = Mathf.FloorToInt(y / multiple) * MeshLabChunk.chunkSize;
        pos.z = Mathf.FloorToInt(z / multiple) * MeshLabChunk.chunkSize;

        MeshLabChunk containerChunk = null;

        chunks.TryGetValue(pos, out containerChunk);

        return containerChunk;
    }

    public Block GetBlock(int x, int y, int z)
    {
        MeshLabChunk containerChunk = GetChunk(x, y, z);

        if (containerChunk != null)
        {
            Block block = containerChunk.GetBlock(
                x - containerChunk.pos.x,
                y - containerChunk.pos.y,
                z - containerChunk.pos.z);

            return block;
        }
        else
        {
            return new BlockAir();
            //return null;
            //return new Block();
        }

    }

    public void SetBlock(int x, int y, int z, Block block)
    {
        MeshLabChunk chunk = GetChunk(x, y, z);

        //Debug.Log("found chunk");

        if (chunk != null)
        {
            //Debug.Log("chunk real");

            chunk.SetBlock(x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, block);
            chunk.update = true;

            UpdateIfEqual(x - chunk.pos.x, 0, new WorldPos(x - 1, y, z));
            UpdateIfEqual(x - chunk.pos.x, MeshLabChunk.chunkSize - 1, new WorldPos(x + 1, y, z));
            UpdateIfEqual(y - chunk.pos.y, 0, new WorldPos(x, y - 1, z));
            UpdateIfEqual(y - chunk.pos.y, MeshLabChunk.chunkSize - 1, new WorldPos(x, y + 1, z));
            UpdateIfEqual(z - chunk.pos.z, 0, new WorldPos(x, y, z - 1));
            UpdateIfEqual(z - chunk.pos.z, MeshLabChunk.chunkSize - 1, new WorldPos(x, y, z + 1));
        }
    }

    void UpdateIfEqual(int value1, int value2, WorldPos pos)
    {
        if (value1 == value2)
        {
            MeshLabChunk chunk = GetChunk(pos.x, pos.y, pos.z);
            if (chunk != null)
                chunk.update = true;
        }
    }

    void Start()
    {
       
    }

    public void GenerateVoxelVer(int cubeSize)
    {

        int numOfChunks = cubeSize / 16;

        for (int x = 0; x < numOfChunks; x++)
        {
            for (int y = 0; y < numOfChunks; y++)
            {
                for (int z = 0; z < numOfChunks; z++)
                {
                    CreateChunk(x * MeshLabChunk.chunkSize, y * MeshLabChunk.chunkSize, z * MeshLabChunk.chunkSize);
                }
            }
        }

        GameObject[] voxelObs = GameObject.FindGameObjectsWithTag("VoxelMesh");
        foreach (GameObject vOb in voxelObs)
        {
            vOb.GetComponent<VoxelObject>().BuildObjectLab();
        }
    }

    public void SetBlock(int x, int y, int z, int block)
    {
        if (block == 0)
        {
            SetBlock(x, y, z, new BlockAir());
        }
        else if (block == 1)
        {
            SetBlock(x, y, z, new Block());
        }
    }

}
