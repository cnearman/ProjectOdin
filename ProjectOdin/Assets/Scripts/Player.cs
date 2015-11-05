using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public GameObject world;

	// Use this for initialization
	void Start () {
        world = GameObject.Find("World");
	}

    public float speed;
    Vector2 rot;

    public GameObject cam;

    public Vector3 GetBlockPos(RaycastHit hit, bool adjacent = false)
    {
        Vector3 pos = new Vector3(
            MoveWithinBlock(hit.point.x, hit.normal.x, adjacent),
            MoveWithinBlock(hit.point.y, hit.normal.y, adjacent),
            MoveWithinBlock(hit.point.z, hit.normal.z, adjacent)
            );

        Vector3 pos2 = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));

        return pos2;
    }

    static float MoveWithinBlock(float pos, float norm, bool adjacent = false)
    {
        if (pos - (int)pos == 0.5f || pos - (int)pos == -0.5f)
        {
            if (adjacent)
            {
                pos += (norm / 2);
            }
            else
            {
                pos -= (norm / 2);
            }
        }

        return (float)pos;
    }


    // Update is called once per frame
    void Update () {
        if(Input.GetButtonDown("Fire1"))
        {
            
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100))
            {

                //Debug.Log("fire");
                Vector3 pos = GetBlockPos(hit);
                Debug.Log(pos);

                world.GetComponent<World>().SetBlock((int) pos.x, (int) pos.y, (int) pos.z, new BlockAir());

                //EditTerrain.SetBlock(hit, new BlockAir());
                /*if (EditTerrain.GetBlock(hit).canDamage)
                {
                    EditTerrain.GetBlock(hit).health -= 1;
                    EditTerrain.GetBlock(hit).TryKill(hit);
                }*/
            }
        }

        if(Input.GetButtonDown("Fire2"))
        {

        }

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
        cam.transform.localRotation = Quaternion.AngleAxis(-rot.y, Vector3.right);
    }
}
