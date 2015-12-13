using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerPropDestruction : NetworkBehaviour {

    [ClientRpc]
    void RpcSphereDamage(Vector3 spherePos, float sphereScale, string prop)
    {
        //GameObject.Find(prop).GetComponent<VoxelProp>().TakeSphereDamageNet(spherePos, sphereScale);
    }


    [Command]
    public void CmdSphereDestroy(Vector3 spherePos, float sphereScale, string prop)
    {
        RpcSphereDamage(spherePos, sphereScale, prop);
        //GameObject.Find(prop).GetComponent<VoxelProp>().TakeSphereDamageNet(spherePos, sphereScale);
    }
}
