using UnityEngine;
using EventSystem;

//[RequireComponent(typeof(BaseModifyBlocks))]
//[RequireComponent(typeof(Movement))]
//[RequireComponent(typeof(Jump))]
public class PlayerControllerChar : BaseClass, EventListener
{

    public int PlayerNumber;
    private BaseModifyBlocks BlockModifier;
    private MovementChar Movement;
    private JumpChar Jump;

    private TriggerOnActivation SpeedBoost;

    public GameObject CameraContainer;

    protected PhotonView m_PhotonView;
    public GameObject cam;
    public PlayerCreateExample pce;
    public PlayerDestroyExample pde;

    public MatchControl mc;

    void Start()
    {
        mc = GameObject.Find("MatchControl").GetComponent<MatchControl>();

        m_PhotonView = GetComponent<PhotonView>();
        if(m_PhotonView.isMine)
        {
            cam.GetComponent<Camera>().enabled = true;
            pce.enabled = true;
            pde.enabled = true;
            GetComponent<TeamTag>().teamNumber = mc.RequestTeam();
            GetComponent<TeamTag>().OnTeamChange();
            mc.RequestSpawn(gameObject);
        }

        EventManager.RegisterListener(this, TypeOfEvent.ButtonEvent);
        EventManager.RegisterListener(this, TypeOfEvent.AxisEvent);
        this.BlockModifier = this.gameObject.GetComponent<BaseModifyBlocks>();
        this.Movement = this.gameObject.GetComponent<MovementChar>();
        this.Movement.CameraContainer = this.CameraContainer;
        this.Jump = this.gameObject.GetComponent<JumpChar>();
        this.SpeedBoost = this.gameObject.GetComponent<TriggerOnActivation>();
    }

    public void EventReceived(BaseEvent e)
    {
        //if (!isLocalPlayer)
        //{
        //    return;
        //}

        if (e.Type == TypeOfEvent.ButtonEvent)
        {
            ButtonEvent bE = e as ButtonEvent;
            if (bE.PlayerNumber == this.PlayerNumber)
            {
                if (bE.ButtonAction == ButtonAction.A)
                {

                }
                else if (bE.ButtonAction == ButtonAction.B)
                {

                }
                else if (bE.ButtonAction == ButtonAction.X)
                {
                    this.JumpAction();
                }
                else if (bE.ButtonAction == ButtonAction.Y)
                {
                    //this.SpeedBoost.CmdActivate();
                }
                else if (bE.ButtonAction == ButtonAction.RightBumper)
                {
                    this.DestroyBlockInRay();
                }
                else if (bE.ButtonAction == ButtonAction.LeftBumper)
                {
                    this.CreateBlockInRay();
                }
            }
        }

        if (e.Type == TypeOfEvent.AxisEvent)
        {
            //Debug.Log("working");
            AxisEvent ae = e as AxisEvent;
            if (ae.PlayerNumber == this.PlayerNumber)
            {
                //Debug.Log("working");
                if (ae.Axis == Axis.MovementX)
                {
                    Movement.MotionInX = ae.Value;
                }
                else if (ae.Axis == Axis.MovementY)
                {
                    Movement.MotionInY = ae.Value;
                }
                else if (ae.Axis == Axis.LookX)
                {
                    
                    Movement.UpdateLateralRotation(ae.Value * 3);
                }
                else if (ae.Axis == Axis.LookY)
                {
                    Movement.UpdateLongitudinalRotation(ae.Value * 3);
                }
            }
        }
    }

    private void JumpAction()
    {
        Jump.PerformJump();
    }

    private void DestroyBlockInRay()
    {
        //BlockModifier.DestroyBlock(CameraContainer.transform.position, CameraContainer.transform.forward);
    }

    private void CreateBlockInRay()
    {
        //BlockModifier.CreateDefaultBlock(CameraContainer.transform.position, CameraContainer.transform.forward);
    }

    Vector3 moveDirection;
    public float speed;
    public float jumpSpeed;
    public float gravity;

    void Update()
    {


        if (m_PhotonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            //moveDirection = Input.GetAxisRaw("Horizontal") * transform.right + Input.GetAxisRaw("Vertical") * transform.forward;//new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //moveDirection.Normalize();
            //moveDirection = transform.TransformDirection(moveDirection);
            //moveDirection *= speed;
            moveDirection = Vector3.zero;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        Vector3 temp = Input.GetAxisRaw("Horizontal") * transform.right + Input.GetAxisRaw("Vertical") * transform.forward;
        temp.y = 0;
        temp.Normalize();
        moveDirection.x = temp.x * speed;
        moveDirection.z = temp.z * speed;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}

