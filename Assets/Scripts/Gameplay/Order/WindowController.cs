using Gameplay.Order;
using Infrastructure.EventManagement;
using Infrastructure.ServiceLocating;
using Logic.GameEvents;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindowController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI elapsedTimeText;
    [SerializeField] GameObject ingredientTextPrefab;
    [SerializeField] Transform ingredientTextParent;
    [SerializeField] TextMeshProUGUI scoreText;


    private int elapsedTime = 0;
    private Order currentActiveOrder;
    private Coroutine OrderTimer;
    private List<GameObject> orderIngredientInfoList;

    // Start is called before the first frame update
    void Awake()
    {
        orderIngredientInfoList = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if (playerController.DoesOwnIngredient())
        {
            var ingredient = playerController.GetOwnedIngredient().ingredient;
            var success = currentActiveOrder.TryDeliver(ingredient.GetType());
            if (success)
            {
                Destroy(playerController.TakeIngredient().ingredientObject);
                playerController.RemoveIngredient();
                ServiceLocator.Find<EventManager>().Propagate(new IngredientDeliveredEvent(), new GameEventData(this.gameObject.name, this));
            }
            
        }
        
    }

    public Order GetCurrentActiveOrder() => currentActiveOrder; 

    public void SetCurrentActiveOrder(Order order)
    {
        currentActiveOrder = order;
        // Add Order ingredients UI
        var ingredientDeliveryStatusList = currentActiveOrder.GetIngredientDeliveryStatusList();
        ClearOrderIngredients();
        StopAllCoroutines();
        foreach (var item in ingredientDeliveryStatusList)
        {
            var ingredientInfoObject = Instantiate(ingredientTextPrefab, ingredientTextParent);
            ingredientInfoObject.GetComponent<TextMeshProUGUI>().text = item.ingredientScore.ingredientType.Name;
            orderIngredientInfoList.Add(ingredientInfoObject);
        }
        elapsedTime = 0;
        OrderTimer = StartCoroutine(StartOrderTimer());
    }

    public void OnOrderDelivered()
    {
        StopCoroutine(OrderTimer);
        ClearOrderIngredients();

        // restart getting order
        StartCoroutine(WaitForNextOrder());
    }

    private void ClearOrderIngredients()
    {
        for (int i = 0; i < orderIngredientInfoList.Count; i++)
        {
            Destroy(orderIngredientInfoList[i]);
            elapsedTimeText.text = 0.ToString();
        }
        orderIngredientInfoList.Clear();
    }

    public void ShowScoreForDelivery(int score)
    {
        string scoreText = "";
        if (score > 0)
        {
            scoreText = "+" + score;
            this.scoreText.color = Color.green;
        }
            
        else if (score < 0)
        {
            scoreText = "-" + score;
            this.scoreText.color = Color.red;
        }
        else
            scoreText = score.ToString();

        this.scoreText.text = scoreText;
        StartCoroutine(WaitAndWipeScore(4, scoreText));
    }

    private IEnumerator StartOrderTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            elapsedTime++;
            elapsedTimeText.text = elapsedTime.ToString();
        }
    }

    private IEnumerator WaitAndWipeScore(float time, string scoreText)
    {
        yield return new WaitForSeconds(time);
        this.scoreText.text = "";
        this.scoreText.color = Color.white;
    }

    private IEnumerator WaitForNextOrder()
    {
        yield return new WaitForSeconds(5);
        ServiceLocator.Find<EventManager>().Propagate(new ReadyForNewOrderEvent(), new GameEventData(this.gameObject.name, this));
    }
}
