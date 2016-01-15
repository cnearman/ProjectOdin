using UnityEngine;
using System.Collections;

public class PlayerCreateExample : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Fire1"))
        {
            GameObject newoby = PhotonNetwork.Instantiate("ball", transform.position + transform.forward * 2f, Quaternion.identity, 0);
        }
	}
}
