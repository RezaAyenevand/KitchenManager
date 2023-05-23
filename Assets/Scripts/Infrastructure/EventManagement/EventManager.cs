using Infrastructure.ServiceLocating;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.EventManagement
{
    public interface EventManager : Service
    {
        void Propagate(GameEvent evt, object data);
        void Register(EventListener listener);
        void UnRegister(EventListener listener);
        bool Has(EventListener listener);
        void Clear();
    }
}
