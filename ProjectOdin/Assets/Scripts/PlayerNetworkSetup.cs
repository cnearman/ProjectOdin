using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    public GameObject myCam;

	// Use this for initialization
	void Start () {
	    if(isLocalPlayer)
        {
            GetComponent<CamMove>().enabled = true;
            myCam.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
