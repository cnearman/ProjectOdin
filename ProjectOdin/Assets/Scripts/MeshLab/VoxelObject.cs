using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class VoxelObject : NetworkBehaviour {
    public int[] blocks;
    public int cubeSide;

    //int[,,] blocksArray;


    public void BuildObject()
    {
        World World = GameObject.Find("World").GetComponent<World>();

        Debug.Log("start print");

        for (int i = 0; i < cubeSide; i++)
        {
            for(int j = 0; j < cubeSide; j++)
            {
                for(int k = 0; k < cubeSide; k++)
                {
                    //Debug.Log(i * cubeSide * cubeSide + j * cubeSide + k);

                    if (blocks[i * cubeSide * cubeSide + j * cubeSide + k] == 1)
                    {
                        //Debug.Log("print");
                        World.SetBlock((int)transform.position.x + i, (int)transform.position.y + j, (int)transform.position.z + k, new Block());
                    }
                }
            }
        }

        Debug.Log("end print");

        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
