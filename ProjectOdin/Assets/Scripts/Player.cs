using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public float speed;
    Vector2 rot;

	// Update is called once per frame
	void Update () {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 move1 = transform.forward * v + transform.right * h;
        move1.Normalize();
        move1 *= speed;

        move1.y = GetComponent<Rigidbody>().velocity.y;

        GetComponent<Rigidbody>().velocity = move1;
        

        rot = new Vector2(
        rot.x + Input.GetAxis("Mouse X") * 3,
        rot.y + Input.GetAxis("Mouse Y") * 3);

        transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
    }
}
