using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MovementSyncPlayer : NetworkBehaviour
{
    public float sendRate;
    float curSendRate;

    // Use this for initialization
    void Start()
    {

    }

    [ClientRpc]
    void RpcChangePos(Vector3 pos)
    {
        transform.position = pos;
    }

    [Command]
    void CmdChangePos(Vector3 pos)
    {
        ChangePos(pos);
    }

    void ChangePos(Vector3 pos)
    {
        if (!isServer)
            return;

        RpcChangePos(pos);
    }

   

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (curSendRate <= 0)
            {
                CmdChangePos(transform.position);
                curSendRate = sendRate;
            }
            else
            {
                curSendRate -= Time.deltaTime;
            }
        }
    }
}
