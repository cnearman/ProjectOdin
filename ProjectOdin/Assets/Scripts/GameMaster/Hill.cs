using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class Hill : NetworkBehaviour {
    public float pointGenRate; //per second

    float currentRate;

    List<GameObject> playersInHill = new List<GameObject>();

    GameObject gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameMaster");
	}
	
	// Update is called once per frame
	void Update () {
        if (isServer)
        {
            if(currentRate <= 0f)
            {
                //Remove destroyed players
                playersInHill.RemoveAll(delegate (GameObject o) { return o == null; });

                //Create a hash set and add the team numbers of all the players to it
                HashSet<int> teamNums = new HashSet<int>();

                foreach(GameObject p in playersInHill)
                {
                    teamNums.Add(p.GetComponent<TeamTag>().teamNumber);
                }

                int[] teamA = teamNums.ToArray();

                //if the length is 1, that means theres only one team in the hill
                if(teamA.Length == 1)
                {
                    //increase the score of this team.
                    gm.GetComponent<GameMaster>().IncreaseScore(teamA[0], 1);
                }

                currentRate = 1f / pointGenRate;
            } else
            {
                currentRate -= Time.deltaTime;
            }

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
