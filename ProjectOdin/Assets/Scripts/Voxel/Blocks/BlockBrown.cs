using UnityEngine;
using System.Collections;
using System;       //Add this line

[Serializable]
public class BlockBrown : Block
{

    public BlockBrown()
        : base()
    {
        blockInt = 10;
    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 4;
        tile.y = 1;

        return tile;
    }
}