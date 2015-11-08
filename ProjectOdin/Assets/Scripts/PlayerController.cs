using System;
using UnityEngine;
using UnityEngine.Networking;
using EventSystem;

[RequireComponent(typeof(BaseModifyBlocks))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Jump))]
public class PlayerController : NetworkBehaviour, EventListener
{

    public int PlayerNumber;
    private BaseModifyBlocks BlockModifier;
    private Movement Movement;
    private Jump Jump;

    public GameObject CameraContainer;

    void Start()
    {
        EventManager.RegisterListener(this, TypeOfEvent.ButtonEvent);
        EventManager.RegisterListener(this, TypeOfEvent.AxisEvent);
        this.BlockModifier = this.gameObject.GetComponent<BaseModifyBlocks>();
        this.Movement = this.gameObject.GetComponent<Movement>();
        this.Movement.CameraContainer = this.CameraContainer; 
        this.Jump = this.gameObject.GetComponent<Jump>();
    }

    public void EventReceived(BaseEvent e)
    {
        if (!isLocalPlayer)
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
        Jump.PerformJump();
    }

    private void DestroyBlockInRay()
    {
        BlockModifier.DestroyBlock(CameraContainer.transform.position, CameraContainer.transform.forward);
    }

    private void CreateBlockInRay()
    {
        BlockModifier.CreateDefaultBlock(CameraContainer.transform.position, CameraContainer.transform.forward);
    }
}

