using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

[RequireComponent(typeof(Targeter))]
[RequireComponent(typeof(Effect))]

public class TriggerOnActivation : BaseNetworkClass {

    public GameObject Owner;

    public Targeter Targeter;
    public Effect[] Effects;

    public void Initialize(GameObject Owner, MonoBehaviour be)
    {
        Targeter = GetComponent<Targeter>();
        this.Owner = Owner;
        Targeter.Owner = Owner;
        Effects = GetComponents<Effect>();
        foreach(Effect e in Effects)
        {
            e.Owner = be;
        }
    }

    public void Activate()
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
