using UnityEngine;
using System.Collections;
using System;       //Add this line

[Serializable]
public class BlockDarkBrown : Block
{

    public BlockDarkBrown()
        : base()
    {
        blockInt = 11;
    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 1;
        tile.y = 1;

        return tile;
    }
}