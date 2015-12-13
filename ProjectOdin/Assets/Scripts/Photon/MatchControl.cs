using UnityEngine;
using System.Collections;

public class MatchControl : MonoBehaviour {

    public int team1Size;
    public int team2Size;

    public int team1Points;
    public int team2Points;

    public int pointsToWin;

    public float totalTime;
    public float currentTime;

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(currentTime);
            stream.SendNext(team1Points);
            stream.SendNext(team2Points);
        }
        else
        {
            currentTime = (float)stream.ReceiveNext();
            team1Points = (int)stream.ReceiveNext();
            team2Points = (int)stream.ReceiveNext();
        }
    }

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
    }

    public void IncreaseScore(int team, int amount)
    {
        if (team == 1)
        {
            team1Points += amount;
        }
        else if (team == 2)
        {
            team2Points += amount;
        }
    }

    public int RequestTeam()
    {
        if (team1Size > team2Size)
        {
            team2Size += 1;
            GetComponent<PhotonView>().RPC("UpdateTeamNums", PhotonTargets.AllBuffered, team1Size, team2Size);
            return 2;
        }
        else
        {
            team1Size += 1;
            UpdateTeamNums(team1Size, team2Size);
            GetComponent<PhotonView>().RPC("UpdateTeamNums", PhotonTargets.AllBuffered, team1Size, team2Size);
            return 1;

        }
    }

    public void RequestSpawn(GameObject p)
    {
        if (p.GetComponent<TeamTag>().teamNumber == 1)
        {
            p.transform.position = new Vector3(0f, 40f, 88f);
            //p.transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else if (p.GetComponent<TeamTag>().teamNumber == 2)
        {
            p.transform.position = new Vector3(0f, 40f, -88f);
        }
    }

    [PunRPC]
    public void UpdateTeamNums(int t1, int t2)
    {
        Debug.Log("recieved team size");
        team1Size = t1;
        team2Size = t2;
    }
}
