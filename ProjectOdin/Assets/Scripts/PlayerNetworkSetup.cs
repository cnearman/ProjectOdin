using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField] GameObject myCam;

	// Use this for initialization
	void Start () {
	    if(isLocalPlayer)
        {
            GetComponent<CamMove>().enabled = true;
            myCam.SetActive(true);
        }
	}
}
