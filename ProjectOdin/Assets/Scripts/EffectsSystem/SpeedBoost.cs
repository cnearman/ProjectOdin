using UnityEngine;
using System.Collections;
using System;

public class SpeedBoost : Effect {

    public float SpeedBoostValue;
    public float Duration;

    public override void ApplyEffect(GameObject target)
    {
        Movement moveComponent = this.GetComponent<Movement>();
        if (moveComponent == null) return;

        moveComponent.Speed += SpeedBoostValue;
        this.StartCoroutine(ResetValue(moveComponent));
    }

    IEnumerator ResetValue(Movement moveComponent)
    {
        yield return this.StartCoroutine(this.WaitForDuration(Duration));
        moveComponent.Speed -= SpeedBoostValue;
    }
}
