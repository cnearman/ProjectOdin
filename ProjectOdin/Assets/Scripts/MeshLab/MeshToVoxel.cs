using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEditor;

public class MeshToVoxel : MonoBehaviour {
    int cubeSide;
    public int[] blocks;

    int i;
    int j;
    int k;

    bool isRunning;
    float buildRate;
    float curBuildRate;
    int probesPerCycle;

    public GameObject probe;
    public GameObject voxObj;

    public GameObject mlWorld;

    public GameObject place;


    public Text estimatedRunTime;
    public InputField cubeSize;
    public InputField cycleTime;
    public InputField numPerCycle;

    public InputField saveName;

    // Use this for initialization
    void Start () {
        cubeSize.text = "" + 16;
        cycleTime.text = "" + 0.05f;
        numPerCycle.text = "" + 16;
	}
	
	// Update is called once per frame
	void Update () {
	    if(isRunning)
        {
            if(curBuildRate <= 0f)
            {
                for (int l = 0; l < probesPerCycle; l++)
                {
                    if (isRunning)
                    {
                        Probe();
                    }
                }

                curBuildRate = buildRate;
            } else
            {
                curBuildRate -= Time.deltaTime;
            }
        } else
        {
            cubeSide = Convert.ToInt32(cubeSize.text);
            buildRate = (float)Convert.ToDouble(cycleTime.text);
            probesPerCycle = Convert.ToInt32(numPerCycle.text);

            estimatedRunTime.text = "Est Run Time: " + (float)(cubeSide * cubeSide * cubeSide) / (float)probesPerCycle * (float)buildRate;
        }
	}

    void Probe()
    {
        bool done = false;

        if (k == cubeSide - 1)
        {
            k = 0;

            if (j == cubeSide - 1)
            {
                j = 0;

                if (i == cubeSide - 1)
                {
                    //were done;
                    done = true;
                }
                else
                {
                    i += 1;
                }

            }
            else
            {
                j += 1;
            }


        }
        else
        {
            k += 1;
        }

        //Debug.Log(i + ", " + j + ", " + k);

        if (!done)
        {
            MakeProbe();
        }
        else
        {
            isRunning = false;
        }
    }

    void MakeProbe()
    {
        GameObject curProbe = (GameObject) Instantiate(probe, new Vector3(i, j, k), Quaternion.identity);
        curProbe.GetComponent<ProbeBlock>().myMaster = gameObject;
    }

    public void VoxelHere(Vector3 pos)
    {
        blocks[(int) pos.x * cubeSide * cubeSide + (int) pos.y * cubeSide + (int) pos.z] = 1;
    }

    public void SaveObject()
    {
        GameObject nVoxelObj = (GameObject)Instantiate(voxObj, Vector3.zero, Quaternion.identity);
        nVoxelObj.GetComponent<VoxelObject>().blocks = blocks;
        nVoxelObj.GetComponent<VoxelObject>().cubeSide = cubeSide;

        

        mlWorld.GetComponent<MeshLabWorld>().GenerateVoxelVer(cubeSide);

        GameObject[] placeHolders = GameObject.FindGameObjectsWithTag("ScanMesh");


        string nameOfOb = saveName.text;
        int meshNum = 0;

        foreach (GameObject ph in placeHolders)
        {

            AssetDatabase.CreateAsset( ph.GetComponent<MeshFilter>().mesh, "Assets/PastryStore/PieMeshes/" + nameOfOb + meshNum + ".asset");
            AssetDatabase.SaveAssets();

            GameObject holder = (GameObject)Instantiate(place, ph.transform.position, Quaternion.identity);
            holder.GetComponent<MeshFilter>().mesh = ph.GetComponent<MeshFilter>().mesh;

            holder.transform.parent = nVoxelObj.transform;

            meshNum += 1;
        }


        

        PrefabUtility.CreatePrefab("Assets/PastryStore/" + nameOfOb + ".prefab", nVoxelObj);
    }

    public void AnalyzeMesh()
    {
        cubeSide = Convert.ToInt32(cubeSize.text);
        buildRate = (float) Convert.ToDouble(cycleTime.text);
        probesPerCycle = Convert.ToInt32(numPerCycle.text);

        estimatedRunTime.text = "Est Run Time: " + (float) (cubeSide * cubeSide * cubeSide) / (float) probesPerCycle * (float) buildRate;

        blocks = new int[cubeSide * cubeSide * cubeSide];

        i = 0;
        j = 0;
        k = -1;

        isRunning = true;
    }

}
