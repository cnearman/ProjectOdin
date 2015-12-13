using UnityEngine;
using System.Collections;

public class PropDestruction : MonoBehaviour {

    public GameObject user;

   
    public void SphereDestroy(Vector3 spherePos, float sphereScale, string prop)
    {
        user.GetComponent<PlayerPropDestruction>().CmdSphereDestroy(spherePos, sphereScale, prop);
    }
}
