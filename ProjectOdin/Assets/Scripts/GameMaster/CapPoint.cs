using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class CapPoint : NetworkBehaviour {

    public float timeToCap;
    public float currentTimeToCap;
    public float decayRate;
    public int teamOwner;

    public float updateRate;
    public float currentRate;

    List<GameObject> playersInHill = new List<GameObject>();

    GameObject gm;
    public GameObject flag;
    float height;

    bool capping;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameMaster");
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (currentRate <= 0f)
            {
                //Remove destroyed players
                playersInHill.RemoveAll(delegate (GameObject o) { return o == null; });

                //Create a hash set and add the team numbers of all the players to it
                HashSet<int> teamNums = new HashSet<int>();

                foreach (GameObject p in playersInHill)
                {
                    teamNums.Add(p.GetComponent<TeamTag>().teamNumber);
                }

                int[] teamA = teamNums.ToArray();

                //if the length is 1, that means theres only one team in the hill
                if (teamA.Length == 1)
                {
                    //if its the team that need to cap, start lowering the flag
                    if(teamA[0] != teamOwner)
                    {
                        capping = true;
                    } else
                    {
                        //put the flag back
                        capping = false;
                    }
                } else
                {
                    //put the flag back
                    capping = false;
                }

                

                if(currentTimeToCap >= timeToCap && teamA.Length == 1 && teamA[0] != teamOwner)
                {
                    gm.GetComponent<GameMaster>().IncreaseScore(teamA[0], 1);
                }

                if(gm.GetComponent<GameMaster>().currentTime <= -1f)
                {
                    gm.GetComponent<GameMaster>().IncreaseScore(teamOwner, 1);
                }

                currentRate = updateRate;
            }
            else
            {
                currentRate -= Time.deltaTime;
            }

            if(capping)
            {
                currentTimeToCap += Time.deltaTime;
            } else
            {
                if (currentTimeToCap > 0f)
                {
                    currentTimeToCap -= Time.deltaTime * decayRate;
                }
            }

            height = 8f - (currentTimeToCap / timeToCap) * 8f;

            flag.transform.localPosition = new Vector3(flag.transform.localPosition.x, height, flag.transform.localPosition.z);

        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (isServer)
        {
            if (other.GetComponent<TeamTag>() != null)
            {
                playersInHill.Add(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isServer)
        {
            if (other.GetComponent<TeamTag>() != null)
            {
                playersInHill.Remove(other.gameObject);
            }
        }
    }
}
