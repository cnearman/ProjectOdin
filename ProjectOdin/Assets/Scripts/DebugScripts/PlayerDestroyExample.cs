using UnityEngine;
using System.Collections;

public class PlayerDestroyExample : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Fire2"))
        {
            GameObject newoby = PhotonNetwork.Instantiate("deathball", transform.position + transform.forward * 5f, Quaternion.identity, 0);
            //newoby.GetComponent<SphereCollider>().enabled = true;
        }
        if(Input.GetButtonDown("Fire3"))
        {
            GameObject newoby = PhotonNetwork.Instantiate("deathbox", transform.position + transform.forward * 10f, Quaternion.identity, 0);
            //GameObject newoby = PhotonNetwork.Instantiate("deathbox", transform.position + transform.forward * 10f, Quaternion.Euler(45f, 45f, 45f), 0);
        }
	}
}
