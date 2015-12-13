using UnityEngine;
using System;

public class SniperCam : BaseClass, Effect
{
    public GameObject SniperCamera;
    public float CameraDuration;

    public MonoBehaviour Owner { get; set; }

    public void ApplyEffect(GameObject target)
    {
        SniperController player = target.GetComponent<SniperController>();
        RaycastHit hit;
        if (Physics.Raycast(player.FirePosition, player.FireDirection.eulerAngles, out hit, 5.0f))
        {
            Instantiate(SniperCamera, hit.point, player.FireDirection);
        }  
    }
}
