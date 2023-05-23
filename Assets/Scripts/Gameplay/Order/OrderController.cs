using Gameplay.IngredientEntities;
using Infrastructure.EventManagement;
using Infrastructure.ServiceLocating;
using Logic.GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Order
{
    public class OrderController : EventListener
    {
        private List<IngredientScore> ingredients;
        private List<Order> activeOrders;
        private List<WindowController> windows;

        private Func<Order, int> calculateScoreFunc;
        public OrderController(List<WindowController> windows, Func<Order, int> calculateScore)
        {
            this.windows = windows;
            calculateScoreFunc = calculateScore;
            ingredients = new List<IngredientScore> { new IngredientScore(typeof(Cheese), 10),
                                                  new IngredientScore(typeof(CookedMeat), 30),
                                                  new IngredientScore(typeof(ChoppedVegetable), 20)};
            activeOrders = new List<Order>();
            ServiceLocator.Find<EventManager>().Register(this);
            InitializeWindows();
        }

        public void InitializeWindows()
        {
            foreach (var item in windows)
            {
                StartNewOrder(item);
            }
        }

        private void StartNewOrder(WindowController window)
        {
            int ingredientsCount = UnityEngine.Random.Range(2, 4);

            var orderIngredients = new List<IngredientDeliveryStatus>();
            for (int i = 0; i < ingredientsCount; i++)
            {
                orderIngredients.Add(new IngredientDeliveryStatus(ingredients[UnityEngine.Random.Range(0, ingredients.Count)], false));
            }
            Order order = new Order(orderIngredients, DateTime.Now);
            activeOrders.Add(order);
            window.SetCurrentActiveOrder(order);
        }


        private void OnItemDeliveredToOrder(WindowController windowController)
        {
            var foundOrder = activeOrders.Select(x => windowController.GetCurrentActiveOrder()).First();

            var success = foundOrder.IsOrderDeliveredCompletely();
            if (success)
            {
                foundOrder.SetEndTime(DateTime.Now);
                windowController.OnOrderDelivered();
                var score = calculateScoreFunc.Invoke(foundOrder);
                windowController.ShowScoreForDelivery(score);
            }

        }

        public void OnEvent(GameEvent evt, object data)
        {
            var eventData = (GameEventData)data;
            if (evt is IngredientDeliveredEvent)
            {
                OnItemDeliveredToOrder((WindowController)eventData.data);
            }
            else if (evt is ReadyForNewOrderEvent)
            {
                StartNewOrder((WindowController)eventData.data);
            }
        }
    }

}




