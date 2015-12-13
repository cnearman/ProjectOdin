using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    //public RPGCamera Camera;
    void OnJoinedRoom()
    {
        CreatePlayerObject();
    }

    void CreatePlayerObject()
    {
        Vector3 position = new Vector3(33.5f, 1.5f, 20.5f);

        GameObject newPlayerObject = PhotonNetwork.Instantiate("SniperPhoton", new Vector3(0f, 40f, 0f), Quaternion.identity, 0);

        //Camera.Target = newPlayerObject.transform;
    }
}
