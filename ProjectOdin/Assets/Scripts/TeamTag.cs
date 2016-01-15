using UnityEngine;
using System.Collections;

public class TeamTag : BaseClass {
    public int teamNumber;
    protected PhotonView m_PhotonView;

    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    public void OnTeamChange()
    {
            GameObject.Find("GameMaster").GetComponent<GameMaster>().myTeam = teamNumber;

            GetComponent<PhotonView>().RPC("UpdateTeam", PhotonTargets.AllBuffered, teamNumber);
    }

    [PunRPC]
    public void UpdateTeam(int team)
    {
        teamNumber = team;
    }
}
