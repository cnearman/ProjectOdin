using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AdvancedNetworkManager : NetworkManager
{


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector3(0,80f,0), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

}