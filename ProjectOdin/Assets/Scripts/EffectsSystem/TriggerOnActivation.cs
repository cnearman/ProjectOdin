using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent(typeof(Targeter))]
[RequireComponent(typeof(Effect))]

public class TriggerOnActivation : BaseNetworkClass {

    private Targeter Targeter;
    private IEnumerable<Effect> Effects;

    void Start()
    {
        Targeter = GetComponent<Targeter>();
        Effects = GetComponents<Effect>();
    }

    [Command]
    public void CmdActivate()
    {
        IEnumerable <GameObject> Targets = Targeter.GetTargets();
        foreach(Effect currentEffect in Effects)
        {
            foreach(GameObject currentTarget in Targets)
            {
                currentEffect.ApplyEffect(currentTarget);
            }
        }
    }
}
