using UnityEngine;
using System.Collections.Generic;

public interface Targeter
{
    GameObject Owner { get; set; }

    IEnumerable<GameObject> GetTargets();
}
