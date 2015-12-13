using UnityEngine;

class MovementPhoton : BaseClass
{
    CharacterController Controller;
    PhotonView m_PhotonView;
    Vector3 moveDirection;

    public GameObject CameraContainer;
    public float speed;
    public float jumpSpeed;
    public float gravity;

    public float MotionInX { get; set; }
    public float MotionInY { get; set; }
    public float RotationLateral { get; set; }
    public float RotationLongitudinal { get; set; }

    public bool IsJumping { get; set; }

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        m_PhotonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (m_PhotonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = Vector3.zero;
            if (IsJumping)
            {
                moveDirection.y = jumpSpeed;
                IsJumping = false;
            }


        }
        Vector3 temp = MotionInX * transform.right + MotionInY * transform.forward;
        temp.y = 0;
        temp.Normalize();
        moveDirection.x = temp.x * speed;
        moveDirection.z = temp.z * speed;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

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

    public void PerformJump()
    {
        IsJumping = true;
    }
}
