using UnityEngine;
using System.Collections.Generic;



public class TargetSelf : BaseClass, Targeter
{
    public GameObject Owner { get; set; }

    public IEnumerable<GameObject> GetTargets()
    {
        return new List<GameObject>{ Owner };
    }
}
