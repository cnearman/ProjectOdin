namespace EventSystem
{
    public partial class TypeOfEvent
    {
        public readonly static TypeOfEvent BaseEvent = new TypeOfEvent("BaseEvent");

        public readonly string Type;

        private TypeOfEvent(string typeName)
        {
            this.Type = typeName;
        }
    }

    /// <summary>
    /// Base Event Object for component communication.
    /// </summary>
    public class BaseEvent
    {
        public TypeOfEvent Type = TypeOfEvent.BaseEvent;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseEvent()
        {
        }
    }
}