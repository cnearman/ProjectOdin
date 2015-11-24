using UnityEngine;
using System.Collections;
using System;       //Add this line

[Serializable]
public class BlockAir : Block
{
    public BlockAir()
        : base()
    {
        blockInt = 0;
        air = true;
    }

    /*public override MeshData Blockdata
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        return meshData;
    }*/

    public override bool IsSolid(Direction direction)
    {
        return false;
    }
}
