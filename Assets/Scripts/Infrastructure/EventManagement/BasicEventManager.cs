using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.EventManagement
{
    public class BasicEventManager : EventManager
    {
        private List<EventListener> listeners = new List<EventListener>();

        public void Register(EventListener listener)
        {
            if (listener == null)
            {
                UnityEngine.Debug.LogError("[BasicEventManager]: Attempted to Register a NULL EventListener");
                return;
            }
            if (listeners.Any(x => x.GetType() == listener.GetType()) == false)
            {
                listeners.Add(listener);
            }

        }
        public void UnRegister(EventListener listener)
        {
            if (listener == null)
            {
                UnityEngine.Debug.LogError("[BasicEventManager]: Attempted to UnRegister a NULL EventListener");
                return;
            }
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }

        }

        public void Propagate(GameEvent evt, object sender)
        {
            List<EventListener> listenersCopy = new List<EventListener>(listeners);
            foreach (var listener in listenersCopy)
                listener.OnEvent(evt, sender);
        }

        public bool Has(EventListener listener)
        {
            return listeners.Contains(listener);
        }

        public void Clear()
        {
            listeners.Clear();
        }

    }
}