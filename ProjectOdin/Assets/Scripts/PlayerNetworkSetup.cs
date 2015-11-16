using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField] Camera myCam;

	// Use this for initialization
	void Start () {
	    if(isLocalPlayer)
        {
            //GetComponent<Player>().enabled = true;
            myCam.enabled = true;
        }

        if(isServer)
        {
            GetComponent<TeamTag>().teamNumber = GameObject.Find("GameMaster").GetComponent<GameMaster>().RequestTeam();
            GameObject.Find("GameMaster").GetComponent<GameMaster>().RequestSpawn(gameObject);
        }
	}

}
