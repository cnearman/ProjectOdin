﻿using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    Vector2 rot;

    void Update()
    {
       

        rot = new Vector2(
        rot.x + Input.GetAxis("Mouse X") * 3,
        rot.y + Input.GetAxis("Mouse Y") * 3);

        transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
        transform.localRotation = Quaternion.AngleAxis(rot.y, Vector3.left);

        transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
        transform.position += transform.right * 3 * Input.GetAxis("Horizontal");
    }
}
