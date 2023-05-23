using Gameplay.IngredientEntities;
using Infrastructure.EventManagement;
using Infrastructure.ServiceLocating;
using Logic.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.KitchenEntities
{
    public class StoveController : AbstractKitchenOperatorController, EventListener
    {

        protected override void Setup()
        {
            ServiceLocator.Find<EventManager>().Register(this);
            inputIngredientType = typeof(RawMeat);
            outputIngredient = new CookedMeat();
            maxSlots = 2;
        }

        public void OnEvent(GameEvent evt, object data)
        {
            var eventData = (GameEventData)data;
            if (eventData.senderName == "StoveTrigger")
            {
                if (evt is ObjectTriggerEnterEvent)
                    OnPlayerTriggeredOperator((PlayerController)eventData.data);

            }
        }

    }

}
