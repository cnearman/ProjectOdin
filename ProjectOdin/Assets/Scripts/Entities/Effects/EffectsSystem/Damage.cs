using UnityEngine;
using System.Collections;
using System;

public class Damage : BaseClass, Effect {

    public int DamageValue;

    public MonoBehaviour Owner { get; set; }

    public void ApplyEffect(GameObject target)
    {
        IDamageable damageTarget = target.GetComponent(typeof(IDamageable)) as IDamageable;
        if (damageTarget == null) return;

        damageTarget.Damage(DamageValue);
    }
}
