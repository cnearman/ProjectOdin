using UnityEngine;
using System.Collections;
using System;       //Add this line

[Serializable]
public class BlockGrass : Block
{

    public BlockGrass()
        : base()
    {
        blockInt = 2;
    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        /*switch (direction)
        {
            case Direction.up:
                tile.x = 2;
                tile.y = 0;
                return tile;
            case Direction.down:
                tile.x = 1;
                tile.y = 0;
                return tile;
        }*/

        tile.x = 7;
        tile.y = 1;

        return tile;
    }
}