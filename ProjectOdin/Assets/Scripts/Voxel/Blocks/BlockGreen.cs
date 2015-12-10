using UnityEngine;
using System.Collections;
using System;       //Add this line

[Serializable]
public class BlockGreen : Block
{

    public BlockGreen()
        : base()
    {
        blockInt = 14;
    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 7;
        tile.y = 1;

        return tile;
    }
}