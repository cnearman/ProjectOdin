using UnityEngine;
using System.Collections;
using System;       //Add this line

[Serializable]
public class BlockGray : Block
{

    public BlockGray()
        : base()
    {
        blockInt = 13;
    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 4;
        tile.y = 4;

        return tile;
    }
}