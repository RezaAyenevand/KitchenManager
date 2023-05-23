using UnityEngine;

namespace Gameplay.IngredientEntities
{
    public class IngredientContainer
    {
        public Ingredient ingredient;
        public GameObject ingredientObject;
        public bool isReady;

        public IngredientContainer(Ingredient ingredient, GameObject ingredientObject, bool isReady)
        {
            this.ingredient = ingredient;
            this.ingredientObject = ingredientObject;
            this.isReady = isReady;
        }
    }
}

