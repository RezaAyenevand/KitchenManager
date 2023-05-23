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
    public class FridgeController : MonoBehaviour, EventListener
    {
        // Start is called before the first frame update
        [SerializeField] GameObject frideItemPopup;
        private List<Type> rawIngeredientTypes;
        private PlayerController playerController;
        private void Start()
        {
            ServiceLocator.Find<EventManager>().Register(this);
            rawIngeredientTypes = new List<Type> { typeof(RawVegetable), typeof(RawMeat), typeof(Cheese) };

        }
        public void OnEvent(GameEvent evt, object data)
        {
            var eventData = (GameEventData)data;

            if (eventData.senderName == "FridgeTrigger")
            {
                playerController = (PlayerController)eventData.data;
                if (evt is ObjectTriggerEnterEvent)
                    ShowFridgeItemsPopup();
                else if (evt is ObjectTriggerExitEvent)
                    HideFridgeItemsPopup();
            }

        }

        private void ShowFridgeItemsPopup()
        {
            frideItemPopup.SetActive(true);
        }

        private void HideFridgeItemsPopup()
        {
            frideItemPopup.SetActive(false);
        }


        public void OnVegetableClicked()
        {
            if (!playerController.DoesOwnIngredient())
            {
                var ingredientObj = ServiceLocator.Find<IngredientFactory>().InstantiateIngredientObject(PrimitiveType.Cube, transform, new Vector3(0, 1.2f, 0));
                var ingredientContainer = new IngredientContainer(new RawVegetable(), ingredientObj, false);
                playerController.SetOwnedIngredient(ingredientContainer);
            }
        }
        public void OnCheeseClicked()
        {
            if (!playerController.DoesOwnIngredient())
            {
                var ingredientObj = ServiceLocator.Find<IngredientFactory>().InstantiateIngredientObject(PrimitiveType.Sphere, transform, new Vector3(0, 1.2f, 0));
                var ingredientContainer = new IngredientContainer(new Cheese(), ingredientObj, false);
                playerController.SetOwnedIngredient(ingredientContainer);
            }
        }
        public void OnMeatClicked()
        {
            if (!playerController.DoesOwnIngredient())
            {
                var ingredientObj = ServiceLocator.Find<IngredientFactory>().InstantiateIngredientObject(PrimitiveType.Cylinder, transform, new Vector3(0, 1.2f, 0));
                var ingredientContainer = new IngredientContainer(new RawMeat(), ingredientObj, false);
                playerController.SetOwnedIngredient(ingredientContainer);
            }
        }



    }

}
