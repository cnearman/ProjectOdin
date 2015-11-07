using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    //The world object in the scene
    public GameObject world;

	// Use this for initialization
    // Grabs the world object
	void Start () {
        world = GameObject.Find("World");
	}

    //player speed and jump speed
    public float speed;
    public float jumpSpeed;

    //player rotation
    Vector2 rot;

    //player cam
    public GameObject cam;

    // Update is called once per frame
    void Update () {
        if (isLocalPlayer) //This ensures we only control our player
        {
            //test block break function
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100))
                {
                    CmdSetBlock(SelectBlock.GetBlockPos(hit, false), 0);
                }
            }

            //test block add function
            if (Input.GetButtonDown("Fire2"))
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100))
                {
                    CmdSetBlock(SelectBlock.GetBlockPos(hit, true), 1);
                }
            }

            if(Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0f, GetComponent<Rigidbody>().velocity.z);
                GetComponent<Rigidbody>().AddForce(new Vector3(0f, jumpSpeed, 0f));
            }

            //get movment inputs
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            //normalize and scale by speed
            Vector3 move1 = transform.forward * v + transform.right * h;
            move1.Normalize();
            move1 *= speed;

            //set y velocity
            move1.y = GetComponent<Rigidbody>().velocity.y;

            //set velocity of player
            GetComponent<Rigidbody>().velocity = move1;

            //get rotation inputs
            rot = new Vector2(
            rot.x + Input.GetAxis("Mouse X") * 3,
            rot.y + Input.GetAxis("Mouse Y") * 3);

            //rotate player and camera appropriatly
            transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
            cam.transform.localRotation = Quaternion.AngleAxis(-rot.y, Vector3.right);
        }
    }

    //This command is the set block command.
    [Command]
    public void CmdSetBlock(WorldPos pos, int block)
    {
        GameObject.Find("World").GetComponent<World>().SetBlock(pos, block);
    }
}
