using Gameplay.IngredientEntities;
using Infrastructure.EventManagement;
using Infrastructure.ServiceLocating;
using Logic.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.KitchenEntities
{
    public class ChopperController : AbstractKitchenOperatorController, EventListener
    {

        protected override void Setup()
        {
            ServiceLocator.Find<EventManager>().Register(this);
            inputIngredientType = typeof(RawVegetable);
            outputIngredient = new ChoppedVegetable();
            maxSlots = 1;
        }


        public void OnEvent(GameEvent evt, object data)
        {
            var eventData = (GameEventData)data;
            if (eventData.senderName == "ChopperTrigger")
            {
                if (evt is ObjectTriggerEnterEvent)
                    OnPlayerTriggeredOperator((PlayerController)eventData.data);
                //else if (evt is ObjectTriggerExitEvent)
                //    HideFridgeItemsPopup();
            }
        }

    }

}
