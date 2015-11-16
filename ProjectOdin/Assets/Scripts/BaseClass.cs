using UnityEngine;
using System.Collections;

public class BaseClass : MonoBehaviour {
    public IEnumerator WaitForDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
