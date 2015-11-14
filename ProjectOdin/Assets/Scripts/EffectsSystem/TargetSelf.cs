using UnityEngine;
using System.Collections.Generic;



public class TargetSelf : Targeter
{
    GameObject Self;

    void Start()
    {
        Self = gameObject;
    }

    public override IEnumerable<GameObject> GetTargets()
    {
        return new List<GameObject>{ Self };
    }
}
