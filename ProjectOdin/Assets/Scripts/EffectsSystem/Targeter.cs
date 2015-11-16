using UnityEngine;
using System.Collections.Generic;

public abstract class Targeter : BaseClass  {

    public abstract IEnumerable<GameObject> GetTargets();
}
