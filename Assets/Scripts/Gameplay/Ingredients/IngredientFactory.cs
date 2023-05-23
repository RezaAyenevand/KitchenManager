using Infrastructure.ServiceLocating;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.IngredientEntities
{
    public class IngredientFactory : MonoBehaviour, Service
    {
        // Start is called before the first frame update
        void Start()
        {
            ServiceLocator.Register(this);
        }

        public GameObject InstantiateIngredientObject(PrimitiveType type, Transform parent, Vector3 localPosition)
        {
            var ingredietObject = GameObject.CreatePrimitive(type);
            ingredietObject.transform.SetParent(parent);
            ingredietObject.transform.localPosition = localPosition;
            Destroy(ingredietObject.gameObject.GetComponent<Collider>());
            return ingredietObject;
        }
    }
}

