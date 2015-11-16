using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameMaster : NetworkBehaviour {
    public int pointsToWin;

    [SyncVar]
    public int teamOnePoints;

    [SyncVar]
    public int teamTwoPoints;
    

    public float timeLimit;
    public float currentTime;

    public float timeCheck;
    public float currentTimeCheck;

    public Text uiMyTeam;
    public Text uiTime;
    public Text uiTeamOnePoints;
    public Text uiTeamTwoPoints;

    public Text uiEndText;

    public Image uiMyTeamBanner;
    public Image uiTeamOneGraph;
    public Image uiTeamTwoGraph;

    public int team1Size;
    public int team2Size;

    public int myTeam;

    public int RequestTeam()
    {
        if(team1Size > team2Size)
        {
            team2Size += 1;
            return 2;
        } else
        {
            team1Size += 1;
            return 1;

        }
    }

    public void RequestSpawn(GameObject p)
    {
        if(p.GetComponent<TeamTag>().teamNumber == 1)
        {
            p.transform.position = new Vector3(0f, 40f, 88f);
        } else if (p.GetComponent<TeamTag>().teamNumber == 2)
        {
            p.transform.position = new Vector3(0f, 40f, -88f);
        }
    }

    [ClientRpc]
    void RpcTime(float curTime)
    {
        currentTime = curTime;
    }

    void UpdateClientTime()
    {
        currentTime -= Time.deltaTime;
    }

    void UpdateServerTime()
    {
        if (currentTimeCheck <= 0f)
        {
            RpcTime(currentTime);
            currentTimeCheck = timeCheck;
            Debug.Log("synch time");
        } else
        {
            currentTimeCheck -= Time.deltaTime;
        }
    }

    public void IncreaseScore(int team, int amount)
    {
        if(team == 1)
        {
            teamOnePoints += amount;
        } else if(team == 2)
        {
            teamTwoPoints += amount;
        }
    }

    void UpdateUI()
    {
        int tempMin = (int)(currentTime / 60f);
        int tempSec = (int)(currentTime % 60f);

        uiTime.text = "Time Remaining: " + tempMin + ":" + tempSec;

        uiTeamOnePoints.text = "" + teamOnePoints;
        uiTeamTwoPoints.text = "" + teamTwoPoints;

        uiTeamOneGraph.fillAmount = ((float) teamOnePoints) / ((float) pointsToWin);
        uiTeamTwoGraph.fillAmount = ((float) teamTwoPoints) / ((float) pointsToWin);

        

        if (myTeam == 1)
        {
            uiMyTeam.text = "let the rivers run RED!";
            uiMyTeamBanner.color = Color.red;
        }
        else if (myTeam == 2)
        {
            uiMyTeam.text = "beat them black and BLUE!";
            uiMyTeamBanner.color = Color.blue;
        }

    }

    // Use this for initialization
    void Start () {
        currentTime = timeLimit;
	}
	
    [ClientRpc]
    void RpcEndGameMessage(int winningTeam)
    {
        if(myTeam == winningTeam)
        {
            uiEndText.text = "You achieved a great victory this day";
        } else
        {
            uiEndText.text = "You may have lost, but revenge will be yours";
        }
    }
    
    void CheckForWin()
    {
        if(teamOnePoints >= pointsToWin)
        {
            RpcEndGameMessage(1);
        } else if(teamTwoPoints >= pointsToWin)
        {
            RpcEndGameMessage(2);
        } else if(currentTime <= 0f)
        {
            if(teamOnePoints > teamTwoPoints)
            {
                RpcEndGameMessage(1);
            } else
            {
                RpcEndGameMessage(2);
            }
        }

    }

	// Update is called once per frame
	void Update () {
        UpdateClientTime();
	    if(isServer)
        {
            UpdateServerTime();
            CheckForWin();
        }
        UpdateUI();
	}
}
