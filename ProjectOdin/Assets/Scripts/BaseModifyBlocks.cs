using System;
using UnityEngine;
using UnityEngine.Networking;

public class BaseModifyBlocks : NetworkBehaviour {
    World World;

    void Start()
    {
        World = GameObject.Find("World").GetComponent<World>();
    }
    
    public void CreateDefaultBlock(Vector3 pos, Vector3 dir)
    {
        this.ModifyBlock(pos, dir, true, 1);
    }

    public void DestroyBlock(Vector3 pos, Vector3 dir)
    {
        this.ModifyBlock(pos, dir, false, 0);
    }

    private void ModifyBlock(Vector3 pos, Vector3 dir, bool adjacent, int state)
    {
        if (World == null)
        {
            throw new ArgumentNullException("World", "Scene lacks an instance of World.");
        }
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, 100))
        {
            SetBlock(SelectBlock.GetBlockPos(hit, adjacent), state);
            //CmdSetBlock(SelectBlock.GetBlockPos(hit, adjacent), state);
        }
    }

    public void SetBlock(WorldPos pos, int block)
    {
        World.SetBlock(pos.x, pos.y, pos.z, block);
        CmdSetBlock(pos, block);
    }

    //This command is the set block command.
    [Command]
    public void CmdSetBlock(WorldPos pos, int block)
    {
       World.SetBlock(pos, block);
    }
}
