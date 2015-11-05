﻿using UnityEngine;
using System.Collections;

public class BlockAir : Block
{
    public BlockAir()
        : base()
    {
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
