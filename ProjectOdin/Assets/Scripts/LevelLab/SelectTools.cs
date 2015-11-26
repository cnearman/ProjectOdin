﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectTools : MonoBehaviour {

    public bool inObjectMenu;
    public bool inBlockMenu;

    public int currentBlock;

    public bool placingObject;
    //public GameObject objectToPlace;
    public GameObject placeTemp;
    public int offsetX;
    public int offsetY;
    public int offsetZ;

    public GameObject blockMenu;
    public GameObject objectMenu;
    public GameObject user;

    public GameObject object0;

	void Start()
    {
       
        blockMenu.SetActive(false);
        objectMenu.SetActive(false);

    }

    public void PickObject(int obj)
    {
        if(obj == 0)
        {
            //objectToPlace = object0;
            placeTemp = (GameObject) Instantiate(object0, user.transform.position, Quaternion.identity);
            offsetX = 0;
            offsetY = 0;
            offsetZ = 0;
            placingObject = true;
        }
    }

    public void PickBlock(int block)
    {
        currentBlock = block;
        if(user != null)
        {
            user.GetComponent<BaseModifyBlocks>().selectedBlock = block;
        }
    }

    public void PullUpBlockMenu()
    {
        if(inBlockMenu)
        {
            inBlockMenu = false;
            blockMenu.SetActive(false);
            user.GetComponent<BaseModifyBlocks>().inMenu = false;
            
        } else
        {
            inBlockMenu = true;
            blockMenu.SetActive(true);
            user.GetComponent<BaseModifyBlocks>().inMenu = true;

        }
    }

    public void PullUpObjectMenu()
    {
        if (inObjectMenu)
        {
            inObjectMenu = false;
            objectMenu.SetActive(false);
            user.GetComponent<BaseModifyBlocks>().inMenu = false;

        }
        else
        {
            inObjectMenu = true;
            objectMenu.SetActive(true);
            user.GetComponent<BaseModifyBlocks>().inMenu = true;

        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("BlockMenu"))
        {
            PullUpBlockMenu();
        }
        if(Input.GetButtonDown("ObjMenu"))
        {
            PullUpObjectMenu();
        }

        if(Input.GetButtonDown("ObjUp"))
        {
            offsetY++;
        }
        if (Input.GetButtonDown("ObjDown"))
        {
            offsetY--;
        }
        if (Input.GetButtonDown("ObjLeft"))
        {
            offsetX--;
        }
        if (Input.GetButtonDown("ObjRight"))
        {
            offsetX++;
        }
        if (Input.GetButtonDown("ObjForward"))
        {
            offsetZ++;
        }
        if (Input.GetButtonDown("ObjBack"))
        {
            offsetZ--;
        }

        if(Input.GetButtonDown("ObjPlace"))
        {
            placingObject = false;
            placeTemp.GetComponent<VoxelObject>().BuildObject();
        }


        if (placingObject)
        {
            
            RaycastHit hit;
            Camera cam = user.GetComponentInChildren<Camera>();
            if (Physics.Raycast(user.transform.position, cam.transform.forward, out hit, 100))
            {
                WorldPos pos = SelectBlock.GetBlockPos(hit, true);
                placeTemp.transform.position = new Vector3(pos.x + offsetX, pos.y + offsetY, pos.z + offsetZ);

                //CmdSetBlock(SelectBlock.GetBlockPos(hit, adjacent), state);
            }
        }

	}
}
