using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MovmentSynchTest : NetworkBehaviour {
    public float sendRate;
    float curSendRate;
    public float speed;

	// Use this for initialization
	void Start () {
	
	}

    [ClientRpc]
    void RpcChangePos(Vector3 pos)
    {
        transform.position = pos;
    }

    void Move()
    {
        if(transform.position.z > 20)
        {
            transform.position = new Vector3(0f, 34f, 0f);
        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * speed);
        }
    }

    // Update is called once per frame
    void Update () {
        if (isServer)
        {
            Move();

            if (curSendRate <= 0)
            {
                RpcChangePos(transform.position);
                curSendRate = sendRate;
            } else
            {
                curSendRate -= Time.deltaTime;
            }
        }
	}
}
