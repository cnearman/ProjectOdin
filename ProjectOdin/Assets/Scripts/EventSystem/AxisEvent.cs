namespace EventSystem
{
    public partial class TypeOfEvent
    {
        public readonly static TypeOfEvent AxisEvent = new TypeOfEvent("AxisEvent");
    }

    /// <summary>
    /// Enum expressing possible Axes that can change
    /// </summary>
    public enum Axis
    {
        None = 0,
        MovementX = 1,
        MovementY = 2,
        LookX = 3,
        LookY = 4
    }

    /// <summary>
    /// Event for communicating Axis Changes to other objects
    /// </summary>
    public class AxisEvent : BaseEvent
    {
        public int PlayerNumber;
        public Axis Axis;
        public float Value;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AxisEvent() : this(0, Axis.None, 0.0f)
        {
        }

        public AxisEvent(int playerNumber, Axis axis, float value)
        {
            this.Type = TypeOfEvent.AxisEvent;
            this.PlayerNumber = playerNumber;
            this.Axis = axis;
            this.Value = value;
        }
    }
}