using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.IngredientEntities
{
    public class IngredientDeliveryStatus
    {
        public IngredientScore ingredientScore;
        public bool isDelivered;
        public IngredientDeliveryStatus(IngredientScore ingredientScore, bool isDelivered)
        {
            this.ingredientScore = ingredientScore;
            this.isDelivered = isDelivered;

        }
    }
}

