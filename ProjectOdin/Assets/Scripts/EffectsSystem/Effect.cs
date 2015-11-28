using UnityEngine;
using System.Collections;

public interface Effect 
{
    MonoBehaviour Owner { get; set; }
    void ApplyEffect(GameObject target);
}
