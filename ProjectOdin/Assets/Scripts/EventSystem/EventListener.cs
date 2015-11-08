namespace EventSystem
{

    /// <summary>
    /// Interface for any components that may listen for events.
    /// </summary>
    public interface EventListener {
        
        /// <summary>
        /// Method for handling events.
        /// </summary>
        /// <param name="e">The event that has been triggered.</param>
        void EventReceived(BaseEvent e);
    }
}