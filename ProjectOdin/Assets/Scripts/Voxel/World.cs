using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class World : BaseClass
{

    public string worldName;

    public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk>();
    public GameObject chunkPrefab;

    public GameObject[] voxObjects;

    public GameObject voxPrefab;

    public void CreateChunk(int x, int y, int z, int type)
    {
        //the coordinates of this chunk in the world
        WorldPos worldPos = new WorldPos(x, y, z);

        //Instantiate the chunk at the coordinates using the chunk prefab
        GameObject newChunkObject = Instantiate(
                        voxPrefab, new Vector3(worldPos.x, worldPos.y, worldPos.z),
                        Quaternion.Euler(Vector3.zero)
                    ) as GameObject;

        //Get the object's chunk component
        VoxelProp newChunk = newChunkObject.GetComponent<VoxelProp>();

        newChunk.MakeArrays(16, 16, 16);

        //Assign its values
        //newChunk.pos = worldPos;
        //newChunk.world = this;

        //Add it to the chunks dictionary with the position as the key
        //chunks.Add(worldPos, newChunk);

        bool loaded = Serialization.Load(newChunk, worldName, worldPos);
        if (loaded)
        {
            newChunk.update = true;
            return;
        }

        /*
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
        */

        /*for (int xi = 0; xi < Chunk.chunkSize; xi++)
        {
            for (int yi = 0; yi < Chunk.chunkSize; yi++)
            {
                for (int zi = 0; zi < Chunk.chunkSize; zi++)
                {

                    if (type != 1)
                    {
                        SetBlock(x + xi, y + yi, z + zi, new BlockAir());
                    }
                    else
                    {

                        if (yi > 14)
                        {
                            SetBlock(x + xi, y + yi, z + zi, new BlockGreen());
                        } else if(yi > 12)
                        {
                            SetBlock(x + xi, y + yi, z + zi, new BlockDarkBrown());
                        }
                        else
                        {
                            SetBlock(x + xi, y + yi, z + zi, new BlockDarkGray());
                        }
                    }
                }
            }
        }*/

        //var terrainGen = new TerrainGen();
        //newChunk = tgen.ChunkGen(newChunk, this);

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

    public TerrainGen tgen;

    void Start()
    {
        tgen = new TerrainGen();
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
                    if (y == -2)
                    {
                        CreateChunk(x * Chunk.chunkSize, y * Chunk.chunkSize, z * Chunk.chunkSize, 1);
                    }
                    else
                    {
                        CreateChunk(x * Chunk.chunkSize, y * Chunk.chunkSize, z * Chunk.chunkSize, 0);
                    }
                }
            }
        }

        tgen.MakeAllTrees();

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
                        //Debug.Log("saving chunk");
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
        else if (block == 2)
        {
            SetBlock(x, y, z, new BlockGrass());
        }
        else if (block == 10)
        {
            SetBlock(x, y, z, new BlockBrown());
        }
        else if (block == 11)
        {
            SetBlock(x, y, z, new BlockDarkBrown());
        }
        else if (block == 12)
        {
            SetBlock(x, y, z, new BlockDarkGray());
        }
        else if (block == 13)
        {
            SetBlock(x, y, z, new BlockGray());
        }
        else if (block == 14)
        {
            SetBlock(x, y, z, new BlockGreen());
        }
        else if (block == 15)
        {
            SetBlock(x, y, z, new BlockLime());
        }
    }

    //[ClientRpc]
    void RpcVoxelObject(WorldPos pos, int obj)
    {

        GameObject vox = (GameObject)Instantiate(voxObjects[obj], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
        vox.GetComponent<VoxelObject>().BuildObject();
    }

    public void MakeObject(WorldPos pos, int obj)
    {
        //if (!isServer)
        //    return;

        RpcVoxelObject(pos, obj);
    }

    //Sets the blocks on the client
    //[ClientRpc]
    void RpcSetBlock(WorldPos pos, int block)
    {
        if (block == 0)
        {
            SetBlock(pos.x, pos.y, pos.z, new BlockAir());
        }
        else if(block == 1)
        {
            SetBlock(pos.x, pos.y, pos.z, new Block());
        }
        else if (block == 2)
        {
            SetBlock(pos.x, pos.y, pos.z, new BlockGrass());
        } else if(block == 10)
        {
            SetBlock(pos.x, pos.y, pos.z, new BlockBrown());
        }
        else if (block == 11)
        {
            SetBlock(pos.x, pos.y, pos.z, new BlockDarkBrown());
        }
        else if (block == 12)
        {
            SetBlock(pos.x, pos.y, pos.z, new BlockDarkGray());
        }
        else if (block == 13)
        {
            SetBlock(pos.x, pos.y, pos.z, new BlockGray());
        }
        else if (block == 14)
        {
            SetBlock(pos.x, pos.y, pos.z, new BlockGreen());
        }
        else if (block == 15)
        {
            SetBlock(pos.x, pos.y, pos.z, new BlockLime());
        }
    }

    //The server sends the calls to the client
    public void SetBlock(WorldPos pos, int block)
    {
       // if (!isServer)//only the server needs to do this
       //     return;

        RpcSetBlock(pos, block);
    }

}
