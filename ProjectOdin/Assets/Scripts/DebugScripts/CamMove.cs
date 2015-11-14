using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    Vector2 rot;

    void Update()
    {


        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 move1 = transform.forward * v + transform.right * h;
        move1.Normalize();
        move1 *= 50;

        move1.y = GetComponent<Rigidbody>().velocity.y;

        GetComponent<Rigidbody>().velocity = move1;

        transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

        rot = new Vector2(
        rot.x + Input.GetAxis("Mouse X") * 3,
        rot.y + Input.GetAxis("Mouse Y") * 3);

        transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);

    }
}
