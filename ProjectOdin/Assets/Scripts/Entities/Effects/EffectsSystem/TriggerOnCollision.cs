using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Effect))]

public class TriggerOnCollision : BaseClass {

    public GameObject Owner { get; set; }

    public IEnumerable<Effect> Effects;

    void Awake()
    {
        Effects = GetComponents<Effect>();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target == null || target == Owner) return;

        foreach(Effect currentEffect in Effects)
        {
            currentEffect.ApplyEffect(target);
        }
    }

}
