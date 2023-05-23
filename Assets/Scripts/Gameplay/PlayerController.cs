using Gameplay.IngredientEntities;
using Infrastructure.ServiceLocating;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IngredientContainer ownedIngredient;

    //private Ingredient ownedIngeredient;
    public float speed = 5f;
    //private GameObject ownedIngredientObj;

    private Rigidbody rb;
    private Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(-moveVertical, 0f, moveHorizontal).normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = movement;
    }


    public void SetOwnedIngredient(IngredientContainer ingredientContainer)
    {
        ownedIngredient = ingredientContainer;
        ownedIngredient.ingredientObject.transform.parent = transform;
        ownedIngredient.ingredientObject.transform.localPosition = new Vector3(0, 1.2f, 0);
    }

    public bool DoesOwnIngredient()
    {
        return ownedIngredient != null;
    }

    public IngredientContainer GetOwnedIngredient() => ownedIngredient;
    public IngredientContainer TakeIngredient()
    {
        return ownedIngredient;
    }
    public void RemoveIngredient()
    {
        ownedIngredient = null;
        
    }

}
