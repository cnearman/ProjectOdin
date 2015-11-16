using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TeamTag : NetworkBehaviour {
    [SyncVar (hook = "OnTeamChange")]
    public int teamNumber;

    void OnTeamChange(int nTeam)
    {
        if(isLocalPlayer)
        {
            GameObject.Find("GameMaster").GetComponent<GameMaster>().myTeam = nTeam;
        }
    }
}
