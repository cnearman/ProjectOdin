using UnityEngine;
using UnityEngine.Networking;

public class MovementChar : NetworkBehaviour
{
    Rigidbody RigidBodyComponent;
    public GameObject CameraContainer;

    void Start()
    {
        RigidBodyComponent = GetComponent<Rigidbody>();
    }


    [SyncVar]
    public float Speed;

    public float MotionInX { get; set; }
    public float MotionInY { get; set; }
    public float RotationLateral { get; set; }
    public float RotationLongitudinal { get; set; }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) { return; }

       

        transform.localRotation = Quaternion.AngleAxis(RotationLateral, Vector3.up);
        if (CameraContainer)
        {
            CameraContainer.transform.localRotation = Quaternion.AngleAxis(-RotationLongitudinal, Vector3.right);
        }
    }

    public void UpdateLateralRotation(float value)
    {
        this.RotationLateral += value;
    }

    public void UpdateLongitudinalRotation(float value)
    {
        this.RotationLongitudinal += value;
    }
}
