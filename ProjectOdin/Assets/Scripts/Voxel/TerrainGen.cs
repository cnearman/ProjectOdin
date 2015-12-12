using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimplexNoise;

public class TerrainGen
{

    //float caveFrequency = 0.025f;
    //int caveSize = 7;

    float treeFrequency = 0.2f;
    int treeDensity = 3;

    float stoneBaseHeight = -24;
    float stoneBaseNoise = 0.05f;
    float stoneBaseNoiseHeight = 3;

    float stoneMountainHeight = 32;//48;
    float stoneMountainFrequency = 0.008f;
    float stoneMinHeight = -24;

    float dirtBaseHeight = 2;
    float dirtNoise = 0.04f;
    float dirtNoiseHeight = 3;

    float grassBaseHeight = 1;

    World world;

    List<Vector3> treeLocs = new List<Vector3>();

    public Chunk ChunkGen(Chunk chunk, World worldp)
    {
        world = worldp;
        //Debug.Log("making");
        for (int x = chunk.pos.x; x < chunk.pos.x + Chunk.chunkSize; x++) //Change this line
        {
            for (int z = chunk.pos.z; z < chunk.pos.z + Chunk.chunkSize; z++)//and this line
            {
                chunk = ChunkColumnGen(chunk, x, z);
            }
        }
        return chunk;
    }

    public Chunk ChunkColumnGen(Chunk chunk, int x, int z)
    {
        int stoneHeight = Mathf.FloorToInt(stoneBaseHeight);
        stoneHeight += GetNoise(x, 0, z, stoneMountainFrequency, Mathf.FloorToInt(stoneMountainHeight));

        if (stoneHeight < stoneMinHeight)
            stoneHeight = Mathf.FloorToInt(stoneMinHeight);

        stoneHeight += GetNoise(x, 0, z, stoneBaseNoise, Mathf.FloorToInt(stoneBaseNoiseHeight));

        //stoneHeight -= 8;

        int dirtHeight = stoneHeight + Mathf.FloorToInt(dirtBaseHeight);
        //dirtHeight += GetNoise(x, 100, z, dirtNoise, Mathf.FloorToInt(dirtNoiseHeight));
        int grassHeight = dirtHeight + Mathf.FloorToInt(grassBaseHeight);

        for (int y = chunk.pos.y - 8; y < chunk.pos.y + Chunk.chunkSize; y++)
        {
            //Get a value to base cave generation on
            //int caveChance = GetNoise(x, y, z, caveFrequency, 100); //Add this line

            if (y <= stoneHeight)// && caveSize < caveChance) //Add caveSize < caveChance
            {
                world.SetBlock(x, y, z, new BlockDarkGray());
            }
            else if (y <= dirtHeight)// && caveSize < caveChance) //Add caveSize < caveChance
            {
                world.SetBlock(x, y, z, new BlockDarkBrown());

                
            } else if(y <= grassHeight)
            {
                world.SetBlock(x, y, z, new BlockGreen());

                if (y == grassHeight && GetNoise(x, 0, z, treeFrequency, 100) < treeDensity)
                {
                    treeLocs.Add(new Vector3(x, y + 1, z));
                    //CreateTree(x, y + 1, z);
                }
            }
            else
            {
                world.SetBlock(x, y, z, new BlockAir());
            }

        }

        return chunk;
    }

    public void MakeAllTrees()
    {
        foreach(Vector3 vec in treeLocs)
        {
            CreateTree((int) vec.x, (int) vec.y, (int) vec.z);
        }
    }

    public static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }

    /*public static void SetBlock(int x, int y, int z, Block block, Chunk chunk, bool replaceBlocks = false)
    {
        x -= chunk.pos.x;
        y -= chunk.pos.y;
        z -= chunk.pos.z;

        if (Chunk.InRange(x) && Chunk.InRange(y) && Chunk.InRange(z))
        {
            if (replaceBlocks || chunk.blocks[x, y, z] == null)
                chunk.SetBlock(x, y, z, block);
        }
    }*/

    void CreateTree(int x, int y, int z)
    {
        //Debug.Log("tree");
        //create leaves
        for (int xi = -2; xi <= 2; xi++)
        {
            for (int yi = 6; yi <= 10; yi++)
            {
                for (int zi = -2; zi <= 2; zi++)
                {
                    if (Mathf.Abs(zi) == 2 && (Mathf.Abs(yi) == 10 || Mathf.Abs(yi) == 6) && Mathf.Abs(xi) == 2)
                    {

                    }
                    else
                    {
                        world.SetBlock(x + xi, y + yi, z + zi, new BlockLime());
                    }
                }
            }
        }

        //create trunk
        for (int yt = 0; yt < 8; yt++)
        {
            world.SetBlock(x, y + yt, z, new BlockBrown());
        }
    }
}