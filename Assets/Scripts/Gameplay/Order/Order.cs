using Gameplay.IngredientEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Order
{
    public class Order
    {
        private List<IngredientDeliveryStatus> ingredients;
        private DateTime startTime;
        private DateTime endTime;

        public Order(List<IngredientDeliveryStatus> ingredients, DateTime startTime)
        {
            this.ingredients = ingredients;
            this.startTime = startTime;
        }
        public DateTime GetStartTime() => startTime;
        public DateTime GetEndTime() => endTime;
        public void SetEndTime(DateTime endTime)
        {
            this.endTime = endTime;
        }

        public List<IngredientDeliveryStatus> GetIngredientDeliveryStatusList() => ingredients;
        public bool TryDeliver(Type ingredientDeliveredType)
        {
            var selectedIngredients = ingredients.Where(x => x.ingredientScore.ingredientType == ingredientDeliveredType && x.isDelivered == false).ToList();

            if (selectedIngredients.Count > 0)
            {
                selectedIngredients[0].isDelivered = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsOrderDeliveredCompletely()
        {
            return ingredients.All(x => x.isDelivered == true);
        }
    }
}

