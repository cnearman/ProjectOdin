using UnityEngine;

public class ForwardMotion : BaseClass, IProjectileMotion
{
    public float Velocity { get; set; }

    void Update()
    {
        transform.Translate(Vector2.up * 1.0f * Time.deltaTime * Velocity);
    }

}
