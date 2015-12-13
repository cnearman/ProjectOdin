using UnityEngine;
using EventSystem;
using System.Collections.Generic;

//[RequireComponent(typeof(BaseModifyBlocks))]
[RequireComponent(typeof(MovementPhoton))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(IWeapon))]
public class PlayerController : BaseClass, EventListener, IDamageable, IHealable
{

    public int PlayerNumber;
    //private BaseModifyBlocks BlockModifier;
    private MovementPhoton Movement;
    private Health Health;

    private CharacterController Controller;

    private IWeapon PrimaryGun;
    private IWeapon SecondaryGun;

    private TriggerOnActivation[] internalAbilities;

    public GameObject[] Abilities;

    public GameObject CameraContainer;

    protected PhotonView m_PhotonView;
    public GameObject cam;

    public MatchControl mc;

    void Awake()
    {
        //this.BlockModifier = this.gameObject.GetComponent<BaseModifyBlocks>();
        this.Controller = GetComponent<CharacterController>();
        this.Movement = this.gameObject.GetComponent<MovementPhoton>();
        this.Movement.CameraContainer = this.CameraContainer;
        this.Health = this.gameObject.GetComponent<Health>();
        IEnumerable<IWeapon> Guns = this.gameObject.GetComponents<IWeapon>();
        foreach(IWeapon currentGun in Guns)
        {
            if (currentGun.GunPriority == GunPriority.Primary)
            {
                this.PrimaryGun = currentGun;
            }
            else if (currentGun.GunPriority == GunPriority.Secondary)
            {
                this.SecondaryGun = currentGun;
            }
        }

        int count = 0;
        internalAbilities = new TriggerOnActivation[Abilities.Length];
        foreach (GameObject ability in Abilities)
        {
            internalAbilities[count] = ability.GetComponent<TriggerOnActivation>();
            internalAbilities[count].Initialize(gameObject, this);
            internalAbilities[count].Owner = gameObject;
            count++;
        }
    }

    void Start()
    {
        mc = GameObject.Find("MatchControl").GetComponent<MatchControl>();

        m_PhotonView = GetComponent<PhotonView>();
        if (m_PhotonView.isMine)
        {
            cam.GetComponent<Camera>().enabled = true;
            GetComponent<TeamTag>().teamNumber = mc.RequestTeam();
            GetComponent<TeamTag>().OnTeamChange();
            mc.RequestSpawn(gameObject);
        }

        EventManager.RegisterListener(this, TypeOfEvent.ButtonEvent);
        EventManager.RegisterListener(this, TypeOfEvent.AxisEvent);
    }

    public void EventReceived(BaseEvent e)
    {
        if (m_PhotonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

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
                    //this.DestroyBlockInRay();
                    //this.Damage(10);
                    //Debug.Log("IsDead: " + IsDead);
                    this.SecondaryGun.Fire(FirePosition, FireDirection);
                }
                else if (bE.ButtonAction == ButtonAction.LeftBumper)
                {
                    //this.CreateBlockInRay();
                    this.PrimaryGun.Fire(FirePosition, FireDirection);
                }
            }
        }

        if (e.Type == TypeOfEvent.AxisEvent)
        {
            AxisEvent ae = e as AxisEvent;
            if (ae.PlayerNumber == this.PlayerNumber)
            {
                if(ae.Axis == Axis.MovementX)
                {
                    Movement.MotionInX = ae.Value;
                }
                else if(ae.Axis == Axis.MovementY)
                {
                    Movement.MotionInY = ae.Value;
                }
                else if(ae.Axis == Axis.LookX)
                {
                    Movement.UpdateLateralRotation(ae.Value * 3);
                }
                else if(ae.Axis == Axis.LookY)
                {
                    Movement.UpdateLongitudinalRotation(ae.Value * 3);
                }
            }
        }
    }

    private void JumpAction()
    {
        Movement.PerformJump();
    }

    /*
    private void DestroyBlockInRay()
    {
        BlockModifier.DestroyBlock(CameraContainer.transform.position, CameraContainer.transform.forward);
    }

    private void CreateBlockInRay()
    {
        BlockModifier.CreateDefaultBlock(CameraContainer.transform.position, CameraContainer.transform.forward);
    }
    */
    
    private void CmdActivateAbility(int abilityNumber)
    {
        //internalAbilities[abilityNumber].Activate();
    }

    public void Damage(int damageValue)
    {
        Health.AddDamage(damageValue);
    }

    public void Heal(int healValue)
    {
        Health.RemoveDamage(healValue);
    }

    public bool IsDead
    {
        get
        {
            return Health.IsDead;
        }
    }

    public Vector3 FirePosition
    {
        get
        {
            Vector3 displacement = FireDirection.eulerAngles;
            Vector3 result = new Vector3
            (
                Mathf.Cos(displacement.x) * Mathf.Cos(displacement.y),
                Mathf.Sin(displacement.x) * Mathf.Cos(displacement.y),
                Mathf.Cos(displacement.y)
            );

            result.Scale(new Vector3(0.2f, 0.2f, 0.2f));
            return transform.position + result;
        }
    }

    public Quaternion FireDirection
    {
        get
        {
            return Quaternion.Euler(transform.GetChild(0).transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
    }

    public Vector3 CurrentVelocity
    {
        get
        {
            return Controller.velocity;
        }
    }
}

