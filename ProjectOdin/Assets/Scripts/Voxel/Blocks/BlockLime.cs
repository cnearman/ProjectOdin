using UnityEngine;
using System.Collections;
using System;       //Add this line

[Serializable]
public class BlockLime : Block
{

    public BlockLime()
        : base()
    {
        blockInt = 15;
    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 10;
        tile.y = 1;

        return tile;
    }
}