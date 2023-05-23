using Infrastructure.EventManagement;
using Infrastructure.ServiceLocating;
using Logic.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.KitchenEntities
{
    public class TrashCanController : MonoBehaviour, EventListener
    {
        private void Start()
        {
            ServiceLocator.Find<EventManager>().Register(this);
        }

        public void OnEvent(GameEvent evt, object data)
        {
            var eventData = (GameEventData)data;
            if (eventData.senderName == "TrashTrigger")
            {
                if (evt is ObjectTriggerEnterEvent)
                    DestroyIngredient((PlayerController)eventData.data);

            }
        }

        private void DestroyIngredient(PlayerController playerController)
        {
            if (playerController.DoesOwnIngredient())
            {
                Destroy(playerController.TakeIngredient().ingredientObject);
                playerController.RemoveIngredient();
            }
        }
    }

}
