using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField] Camera myCam;
    public bool levelLab;

	// Use this for initialization
	void Start () {
	    if(isLocalPlayer)
        {
            //GetComponent<Player>().enabled = true;
            myCam.enabled = true;

            if(levelLab)
            {
                GameObject.Find("LevelTools").GetComponent<SelectTools>().user = gameObject;
            }

            if(GameObject.Find("PropDestruction") != null)
            {
                GameObject.Find("PropDestruction").GetComponent<PropDestruction>().user = gameObject;
            }

        }

        if(isServer)
        {
            if (levelLab)
            {
                gameObject.transform.position = new Vector3(0f, 40f, 0f);
            }
            else
            {
                GetComponent<TeamTag>().teamNumber = GameObject.Find("GameMaster").GetComponent<GameMaster>().RequestTeam();
                GameObject.Find("GameMaster").GetComponent<GameMaster>().RequestSpawn(gameObject);
            }
        }
	}

}
