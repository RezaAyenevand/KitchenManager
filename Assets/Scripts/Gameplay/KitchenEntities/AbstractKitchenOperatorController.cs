
using Gameplay.IngredientEntities;
using Gameplay.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.KitchenEntities
{
    public abstract class AbstractKitchenOperatorController : MonoBehaviour
    {
        private List<IngredientContainer> ownedIngredients;
        private List<(SliderController, IngredientContainer)> slidersOfIngredients;
        [SerializeField] protected GameObject table;
        [SerializeField] protected int maxSlots;
        [SerializeField] protected Material readyIngredientMaterial;
        [SerializeField] protected float operationTime;
        [SerializeField] private GameObject sliderPrefab;
        protected Type inputIngredientType;
        protected Ingredient outputIngredient;

        protected bool isIngredientReady = false;

        void Start()
        {
            ownedIngredients = new List<IngredientContainer>();
            slidersOfIngredients = new List<(SliderController, IngredientContainer)>();
            Setup();
        }

        protected virtual void Setup() { }


        protected void OnPlayerTriggeredOperator(PlayerController playerController)
        {
            if (playerController.DoesOwnIngredient())
            {
                if (ownedIngredients.Count < maxSlots)
                {
                    if (playerController.GetOwnedIngredient().ingredient.GetType() == inputIngredientType)
                    {
                        var ingredientContainer = playerController.TakeIngredient();
                        AddToOwnedIngredients(ingredientContainer);
                        playerController.RemoveIngredient();
                        var slider = slidersOfIngredients.First(x => x.Item2 == ingredientContainer).Item1;
                        StartCoroutine(Operate(ingredientContainer, operationTime, slider));
                    }
                }
            }
            else
            {
                if (ownedIngredients.Count > 0)
                {
                    if (IsAnyIngredientReady())
                    {
                        var readyIngredient = ownedIngredients.First(x => x.isReady);
                        playerController.SetOwnedIngredient(readyIngredient);
                        ownedIngredients.Remove(readyIngredient);
                        var sliderofIngredient = slidersOfIngredients.First(x => x.Item2 == readyIngredient);
                        Destroy(sliderofIngredient.Item1.gameObject);
                        slidersOfIngredients.Remove(sliderofIngredient);
                    }
                }
            }



        }
        private void AddToOwnedIngredients(IngredientContainer ingredientContainer)
        {
            ownedIngredients.Add(ingredientContainer);

            SetPosition(ingredientContainer, ownedIngredients.IndexOf(ingredientContainer));
            SetupSlider(ingredientContainer);
        }

        private void SetupSlider(IngredientContainer ingredientContainer)
        {
            var slider = Instantiate(sliderPrefab, ingredientContainer.ingredientObject.transform);
            slidersOfIngredients.Add((slider.GetComponentInChildren<SliderController>(), ingredientContainer));
            slider.transform.localPosition = new Vector3(1f, 0, 0);

        }

        private void SetPosition(IngredientContainer ingredientContainer, int index)
        {

            ingredientContainer.ingredientObject.transform.parent = transform;

            var tableLength = (int)table.transform.localScale.z;
            int zAdditive = tableLength - (tableLength / 2 + 1);
            float zPosition = index * 2 - zAdditive;

            ingredientContainer.ingredientObject.transform.localPosition = new Vector3(0, 1f, zPosition / 4);
        }
        private bool IsAnyIngredientReady()
        {
            foreach (var item in ownedIngredients)
            {
                if (item.isReady)
                    return true;
            }
            return false;
        }

        private IEnumerator Operate(IngredientContainer ingredientContainer, float time, SliderController slider)
        {
            int intervals = 20;
            for (int i = 0; i < intervals; i++)
            {
                yield return new WaitForSeconds(time / intervals);
                slider.FillSlider(1f / intervals);
            }

            ingredientContainer.ingredientObject.GetComponent<MeshRenderer>().material = readyIngredientMaterial;
            ingredientContainer.ingredient = outputIngredient;
            ingredientContainer.isReady = true;
        }


    }

}
