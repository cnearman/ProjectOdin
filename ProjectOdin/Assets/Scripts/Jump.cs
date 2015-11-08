using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour {
    public float JumpSpeed;
    Rigidbody RigidBodyComponent;

    void Start()
    {
        RigidBodyComponent = this.GetComponent<Rigidbody>();
    }

    public void PerformJump()
    {
        RigidBodyComponent.velocity = new Vector3(
            RigidBodyComponent.velocity.x,
            0.0f,
            RigidBodyComponent.velocity.z
        );
        RigidBodyComponent.AddForce(new Vector3(0.0f, JumpSpeed, 0.0f));
    }
}
