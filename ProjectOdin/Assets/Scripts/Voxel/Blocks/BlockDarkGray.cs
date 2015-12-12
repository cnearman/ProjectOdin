using UnityEngine;
using System.Collections;
using System;       //Add this line

[Serializable]
public class BlockDarkGray : Block
{

    public BlockDarkGray()
        : base()
    {
        blockInt = 12;
    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 1;
        tile.y = 4;

        return tile;
    }
}