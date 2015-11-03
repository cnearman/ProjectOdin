using UnityEngine;
using System.Collections;

public class BlockAir : Block {

    public override bool IsSolid(Direction direction)
    {
       

        return false;
    }

}
