using UnityEngine;
using System.Collections;

//This class is used to select blocks. Currently only works with raycast hits,
//may add more functions later.

public static class SelectBlock{

    //This gets a block from the passed in raycast hit. If adjacent is false
    //then the position returned is the block hit. If adjacent is true
    //the the position returned is the one adjacent to the block hit
    public static WorldPos GetBlockPos(RaycastHit hit, bool adjacent)
    {
        Vector3 pos = new Vector3(
            MoveWithinBlock(hit.point.x, hit.normal.x, adjacent),
            MoveWithinBlock(hit.point.y, hit.normal.y, adjacent),
            MoveWithinBlock(hit.point.z, hit.normal.z, adjacent)
            );

        WorldPos blockPos = new WorldPos(
             Mathf.RoundToInt(pos.x),
             Mathf.RoundToInt(pos.y),
             Mathf.RoundToInt(pos.z)
             );

        return blockPos;
    }

    //helper function
    static float MoveWithinBlock(float pos, float norm, bool adjacent)
    {
        if (pos - (int)pos == 0.5f || pos - (int)pos == -0.5f)
        {
            if (adjacent)
            {
                pos += (norm / 2);
            }
            else
            {
                pos -= (norm / 2);
            }
        }

        return (float)pos;
    }
}
