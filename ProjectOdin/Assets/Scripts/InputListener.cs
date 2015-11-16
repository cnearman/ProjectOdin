using UnityEngine;
using EventSystem;

public class InputListener : MonoBehaviour {

    public int PlayerNumber;

    public string ButtonX = "X_";
    public string ButtonY = "Y_";
    public string ButtonA = "A_";
    public string ButtonB = "B_";
    public string RightBumper = "RB_";
    public string LeftBumper = "LB_";

    public string LeftTrigger = "TriggersL_";
    public string RightTrigger = "TriggersR_";
    public string LeftStickXAxis = "L_XAxis_";
    public string LeftStickYAxis = "L_YAxis_";


    void Update() {// FixedUpdate () {
        if (Input.GetButtonDown("Fire1"))
        {
            EventManager.Broadcast(new ButtonEvent(1, ButtonAction.RightBumper));
        }
        if (Input.GetButtonDown("Fire2"))
        {
            EventManager.Broadcast(new ButtonEvent(1, ButtonAction.LeftBumper));
        }
        if (Input.GetButtonDown("Jump"))
        {
            EventManager.Broadcast(new ButtonEvent(1, ButtonAction.X));
        }

        EventManager.Broadcast(new AxisEvent(1, Axis.MovementX, Input.GetAxis("Horizontal")));
        EventManager.Broadcast(new AxisEvent(1, Axis.MovementY, Input.GetAxis("Vertical")));
        EventManager.Broadcast(new AxisEvent(1, Axis.LookX, Input.GetAxis("Mouse X")));
        EventManager.Broadcast(new AxisEvent(1, Axis.LookY, Input.GetAxis("Mouse Y")));

        //Default Controller 
        /*if (Input.GetButtonDown(ButtonX + PlayerNumber))
        {
            EventManager.Broadcast(new ButtonEvent(this.PlayerNumber, ButtonAction.X));
        }

        if (Input.GetButtonDown(ButtonY + PlayerNumber))
        {
            EventManager.Broadcast(new ButtonEvent(this.PlayerNumber, ButtonAction.Y));
        }

        if (Input.GetButtonDown(ButtonA + PlayerNumber))
        {
            EventManager.Broadcast(new ButtonEvent(this.PlayerNumber, ButtonAction.A));
        }

        if (Input.GetButtonDown(ButtonB + PlayerNumber))
        {
            EventManager.Broadcast(new ButtonEvent(this.PlayerNumber, ButtonAction.B));
        }

        if (Input.GetButtonDown(RightBumper + PlayerNumber))
        {
            EventManager.Broadcast(new ButtonEvent(this.PlayerNumber, ButtonAction.RightBumper));
        }

        if (Input.GetButtonDown(LeftBumper + PlayerNumber))
        {
            EventManager.Broadcast(new ButtonEvent(this.PlayerNumber, ButtonAction.LeftBumper));
        }*/
    }
}
