using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.IngredientEntities
{
    public class IngredientScore
    {
        public Type ingredientType;
        public int score;

        public IngredientScore(Type ingredientType, int score)
        {
            this.ingredientType = ingredientType;
            this.score = score;
        }
    }
}

