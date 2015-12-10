using UnityEngine;
using System.Collections;
using System;

public class SpeedBoost : BaseClass, Effect {

    public MonoBehaviour Owner { get; set; }

    public float SpeedBoostValue;
    public float Duration;

    public void ApplyEffect(GameObject target)
    {
        Movement moveComponent = target.GetComponent<Movement>();
        if (moveComponent == null) return;

        moveComponent.Speed += SpeedBoostValue;
        Owner.StartCoroutine(ResetValue(moveComponent));
    }

    IEnumerator ResetValue(Movement moveComponent)
    {
        yield return Owner.StartCoroutine(this.WaitForDuration(Duration));
        moveComponent.Speed -= SpeedBoostValue;
    }
}
