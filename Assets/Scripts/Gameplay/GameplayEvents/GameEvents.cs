

using Infrastructure.EventManagement;

namespace Logic.GameEvents
{
    public class ObjectTriggerEnterEvent: GameEvent { }
    public class ObjectTriggerExitEvent : GameEvent { }

    public class IngredientDeliveredEvent : GameEvent { }
    public class ReadyForNewOrderEvent : GameEvent { }


    public class GameEventData
    {
        public string senderName;
        public object data;

        public GameEventData(string senderName, object data)
        {
            this.senderName = senderName;
            this.data = data;
        }
    }
}
