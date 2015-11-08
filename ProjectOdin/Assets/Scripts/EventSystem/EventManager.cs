using System;
using System.Linq;
using System.Collections.Generic;

namespace EventSystem {
    
    /// <summary>
    /// Class for managing the movement of events between components.
    /// </summary>
    public class EventManager {

        /// <summary>
        /// List of entities that are listening for events.
        /// </summary>
        static Dictionary<TypeOfEvent, List<WeakReference>> listeners = new Dictionary<TypeOfEvent, List<WeakReference>>();

        /// <summary>
        /// Method for registering an object that implements the EventListener interface.
        /// </summary>
        /// <param name="el">An object to register for event listening.</param>
        public static void RegisterListener(EventListener el, TypeOfEvent type)
        {
            if (!listeners.ContainsKey(type))
            {
                listeners.Add(type, new List<WeakReference>());
            }
            listeners[type].Add(new WeakReference(el));
        }

        /// <summary>
        /// Method for broadcasting events to event listeners
        /// </summary>
        /// <param name="e">The event being broadcast.</param>
        public static void Broadcast(BaseEvent e)
        {
            //Remove all listeners that no longer have references.
            TypeOfEvent currentEventType = e.Type;
            if (listeners.ContainsKey(currentEventType))
            {
                listeners[currentEventType] = listeners[currentEventType].Where(x => x.IsAlive).ToList();
                foreach (WeakReference listenerElement in listeners[currentEventType])
                {
                    (listenerElement.Target as EventListener).EventReceived(e);
                }
            }
        }

        /// <summary>
        /// Method for unregistering an EventListener.
        /// </summary>
        /// <param name="el">An object to register for event listening.</param>
        public static void UnregisterListener(EventListener el)
        {
            foreach (List<WeakReference> currentList in listeners.Values)
            {
                currentList.RemoveAll(x => x.Target == el);
            }
        }
    }
}