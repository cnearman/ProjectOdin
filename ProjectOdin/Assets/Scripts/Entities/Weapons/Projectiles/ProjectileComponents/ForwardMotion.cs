using UnityEngine;

public class ForwardMotion : BaseClass, IProjectileMotion
{
    public float Velocity;

    void Update() 
    {
        transform.Translate(Vector3.forward * 1.0f * Time.deltaTime * Velocity);
    }

}
