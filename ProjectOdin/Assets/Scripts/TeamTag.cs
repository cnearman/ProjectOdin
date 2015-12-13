using UnityEngine;
using System.Collections;

public class TeamTag : BaseClass {
    //[SyncVar (hook = "OnTeamChange")]
    public int teamNumber;

    void OnTeamChange(int nTeam)
    {
       
            GameObject.Find("GameMaster").GetComponent<GameMaster>().myTeam = nTeam;
        
    }
}
