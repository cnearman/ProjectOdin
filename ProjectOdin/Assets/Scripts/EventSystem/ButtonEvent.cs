namespace EventSystem
{
    public partial class TypeOfEvent
    {
        public readonly static TypeOfEvent ButtonEvent = new TypeOfEvent("ButtonEvent");
    }

    /// <summary>
    /// Enum containing all button presses.
    /// </summary>
    public enum ButtonAction
    {
        None = 0,
        A = 1,
        B = 2,
        X = 3,
        Y = 4,
        RightBumper = 5,
        LeftBumper = 6,
        DPadUp  = 7,
        DPadRight = 8,
        DPadDown = 9,
        DPadLeft= 10
    }

    /// <summary>
    /// Event for communicating button presses to other objects.
    /// </summary>
    public class ButtonEvent : BaseEvent
    {
        public int PlayerNumber;
        public ButtonAction ButtonAction;
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ButtonEvent() : this(0, ButtonAction.None)
        {
        }
        
        public ButtonEvent(int playerNumber, ButtonAction buttonAction)
        {
            this.Type = TypeOfEvent.ButtonEvent;
            this.PlayerNumber = playerNumber;
            this.ButtonAction = buttonAction;
        }
    }
}