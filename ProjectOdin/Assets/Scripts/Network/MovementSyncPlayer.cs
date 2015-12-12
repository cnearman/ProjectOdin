using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MovementSyncPlayer : NetworkBehaviour
{
    public float sendRate;
    float curSendRate;
    int myName;

    // Use this for initialization
    void Start()
    {
        if(isLocalPlayer)
        {
            myName = Random.Range(10, 1000);
        } else
        {
            myName = 0;
        }
    }

    [ClientRpc]
    void RpcChangePos(Vector3 pos, int na)
    {
        if (myName != na)
        {
            transform.position = pos;
        }
    }

    [Command]
    void CmdChangePos(Vector3 pos, int na)
    {
        //Debug.Log("Send");
        ChangePos(pos, na);
    }

    void ChangePos(Vector3 pos, int na)
    {
        if (!isServer)
            return;

        RpcChangePos(pos, na);
    }

   

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (curSendRate <= 0)
            {
                CmdChangePos(transform.position, myName);
                curSendRate = sendRate;
            }
            else
            {
                curSendRate -= Time.deltaTime;
            }
        }
    }
}
