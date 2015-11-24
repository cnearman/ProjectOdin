using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class World : NetworkBehaviour
{

    public string worldName;

    public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
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
        Chunk newChunk = newChunkObject.GetComponent<Chunk>();

        //Assign its values
        newChunk.pos = worldPos;
        newChunk.world = this;

        //Add it to the chunks dictionary with the position as the key
        chunks.Add(worldPos, newChunk);

        bool loaded = Serialization.Load(newChunk);
        if (loaded)
        {
            newChunk.update = true;
            return;
        }

        for (int xi = 0; xi < Chunk.chunkSize; xi++)
        {
            for (int yi = 0; yi < Chunk.chunkSize; yi++)
            {
                for (int zi = 0; zi < Chunk.chunkSize; zi++)
                {
                    if (yi > 7)
                    {
                        SetBlock(x + xi, y + yi, z + zi, new BlockGrass());
                        //SetBlock(x + xi, y + yi, z + zi, new BlockAir());
                    }
                    else
                    {
                        SetBlock(x + xi, y + yi, z + zi, new Block());
                        //SetBlock(x + xi, y + yi, z + zi, new BlockAir());
                    }
                }
            }
        }

        newChunk.SetBlocksUnmodified();
    }

    public void DestroyChunk(int x, int y, int z)
    {
        Chunk chunk = null;
        if (chunks.TryGetValue(new WorldPos(x, y, z), out chunk))
        {
            //Serialization.SaveChunk(chunk);
            UnityEngine.Object.Destroy(chunk.gameObject);
            chunks.Remove(new WorldPos(x, y, z));
        }
    }

    // Use this for initialization


    public Chunk GetChunk(int x, int y, int z)
    {
        WorldPos pos = new WorldPos();
        float multiple = Chunk.chunkSize;
        pos.x = Mathf.FloorToInt(x / multiple) * Chunk.chunkSize;
        pos.y = Mathf.FloorToInt(y / multiple) * Chunk.chunkSize;
        pos.z = Mathf.FloorToInt(z / multiple) * Chunk.chunkSize;

        Chunk containerChunk = null;

        chunks.TryGetValue(pos, out containerChunk);

        return containerChunk;
    }

    public Block GetBlock(int x, int y, int z)
    {
        Chunk containerChunk = GetChunk(x, y, z);

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
        Chunk chunk = GetChunk(x, y, z);

        //Debug.Log("found chunk");

        if (chunk != null)
        {
            //Debug.Log("chunk real");

            chunk.SetBlock(x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, block);
            chunk.update = true;

            UpdateIfEqual(x - chunk.pos.x, 0, new WorldPos(x - 1, y, z));
            UpdateIfEqual(x - chunk.pos.x, Chunk.chunkSize - 1, new WorldPos(x + 1, y, z));
            UpdateIfEqual(y - chunk.pos.y, 0, new WorldPos(x, y - 1, z));
            UpdateIfEqual(y - chunk.pos.y, Chunk.chunkSize - 1, new WorldPos(x, y + 1, z));
            UpdateIfEqual(z - chunk.pos.z, 0, new WorldPos(x, y, z - 1));
            UpdateIfEqual(z - chunk.pos.z, Chunk.chunkSize - 1, new WorldPos(x, y, z + 1));
        }
    }

    void UpdateIfEqual(int value1, int value2, WorldPos pos)
    {
        if (value1 == value2)
        {
            Chunk chunk = GetChunk(pos.x, pos.y, pos.z);
            if (chunk != null)
                chunk.update = true;
        }
    }

    void Start()
    {
        /*for (int x = -8; x < 8; x++)
        {
            for (int y = 1; y < 3; y++)
            {
                for (int z = -16; z < 16; z++)
                {
                    CreateChunk(x * Chunk.chunkSize, y * Chunk.chunkSize, z * Chunk.chunkSize);
                }
            }
        }*/

        //first make the map

        for (int x = -4; x < 4; x++)
        {
            for (int y = -2; y < 2; y++)
            {
                for (int z = -8; z < 8; z++)
                {
                    CreateChunk(x * Chunk.chunkSize, y * Chunk.chunkSize, z * Chunk.chunkSize);
                }
            }
        }

        //now make the object
        GameObject[] voxelObs = GameObject.FindGameObjectsWithTag("VoxelMesh");
        foreach(GameObject vOb in voxelObs)
        {
            vOb.GetComponent<VoxelObject>().BuildObject();
        }


    }

    public void SaveAll()
    {
        for (int x = -4; x < 4; x++)
        {
            for (int y = -2; y < 2; y++)
            {
                for (int z = -8; z < 8; z++)
                {
                    Chunk chunk = null;
                    if (chunks.TryGetValue(new WorldPos(x * Chunk.chunkSize, y * Chunk.chunkSize, z * Chunk.chunkSize), out chunk))
                    {
                        Serialization.SaveChunk(chunk);
                        Debug.Log("saving chunk");
                    }
                }
            }
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

    //Sets the blocks on the client
    [ClientRpc]
    void RpcSetBlock(WorldPos pos, int block)
    {
        if (block == 0)
        {
            SetBlock(pos.x, pos.y, pos.z, new BlockAir());
        } else if(block == 1)
        {
            SetBlock(pos.x, pos.y, pos.z, new Block());
        }
    }

    //The server sends the calls to the client
    public void SetBlock(WorldPos pos, int block)
    {
        if (!isServer)//only the server needs to do this
            return;

        RpcSetBlock(pos, block);
    }

}
