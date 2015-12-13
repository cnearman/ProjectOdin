using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CapPoint : BaseClass {

    public float timeToCap;
    public float currentTimeToCap;
    public float decayRate;
    public int teamOwner;

    List<GameObject> playersInHill = new List<GameObject>();

    public GameObject flag;
    //[SyncVar]
    float height;

    bool capping;

    MatchControl mc;

    protected PhotonView m_PhotonView;

    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
        mc = GameObject.Find("MatchControl").GetComponent<MatchControl>();
    }

    // Update is called once per frame
    void Update() {

        if (m_PhotonView.isMine == false && PhotonNetwork.connected == true)
        {

            flag.transform.localPosition = new Vector3(flag.transform.localPosition.x, height, flag.transform.localPosition.z);
            return;
        }

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
                mc.IncreaseScore(teamA[0], 1);
            }

            if(mc.currentTime <= -1f)
            {
                mc.IncreaseScore(teamOwner, 1);
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

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(height);
        }
        else
        {
            height = (float)stream.ReceiveNext();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if (isServer)
        //{
            if (other.GetComponent<TeamTag>() != null)
            {
                playersInHill.Add(other.gameObject);
            }
        //}
    }

    void OnTriggerExit(Collider other)
    {
        //if (isServer)
        //{
            if (other.GetComponent<TeamTag>() != null)
            {
                playersInHill.Remove(other.gameObject);
            }
        //}
    }
}
