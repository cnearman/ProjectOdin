using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Targeter))]
[RequireComponent(typeof(Effect))]

public class TriggerOnActivation : BaseClass {

    private Targeter Targeter;
    private IEnumerable<Effect> Effects;

    void Start()
    {
        Targeter = GetComponent<Targeter>();
        Effects = GetComponents<Effect>();
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
