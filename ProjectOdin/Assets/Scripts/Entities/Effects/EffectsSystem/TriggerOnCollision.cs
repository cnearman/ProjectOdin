using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Effect))]

public class TriggerOnCollision : BaseClass {

    private IEnumerable<Effect> Effects;

    void Awake()
    {
        Effects = GetComponents<Effect>();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target == null) return;

        foreach(Effect currentEffect in Effects)
        {
            currentEffect.ApplyEffect(target);
        }
    }

}
