using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OnlineObjects : NetworkBehaviour {

    [Command]
    public void CmdSpawnVoxelObject(WorldPos pos, int obj)
    {
        GameObject.Find("World").GetComponent<World>().MakeObject(pos, obj);
    }

}
