using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Effect))]

public class TriggerOnCollision : BaseClass {

    private IEnumerable<Effect> Effects;

    void Start()
    {
        Effects = GetComponents<Effect>();
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;
        if (target == null) return;

        foreach(Effect currentEffect in Effects)
        {
            currentEffect.ApplyEffect(target);
        }
    }

}
