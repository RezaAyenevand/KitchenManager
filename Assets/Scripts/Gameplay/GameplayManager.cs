using Gameplay.Order;
using Gameplay.Score;
using Gameplay.UI;
using Infrastructure.EventManagement;
using Infrastructure.ServiceLocating;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] int gameTime;
        [SerializeField] GameObject WindowPrefab;
        [SerializeField] Transform kitchenItemsParent;

        [SerializeField] GameObject frdgePrefab;
        [SerializeField] GameObject oneSlotChopperPrefab;
        [SerializeField] GameObject twoSlotStovePrefab;
        [SerializeField] GameObject trashCanPrefab;

        private HUDController hudController;
        private GameObject hudPrefab;
        private List<WindowController> windowControllers;
        private OrderController orderController;
        private ScoreController scoreController;

        private void Awake()
        {
            windowControllers = new List<WindowController>();
            hudPrefab = Resources.Load("HUD") as GameObject;
            hudController = Instantiate(hudPrefab).GetComponent<HUDController>();
        }

        private void Start()
        {
            InitializeWindows();
            SetupKitchenOperators();
            hudController.StartGameTimer(gameTime, OnGameTimerFinished);

            scoreController = new ScoreController(hudController.UpdateScore);
            orderController = new OrderController(windowControllers, scoreController.CalculateScore);
            hudController.SetHighScore(scoreController.GetHighScore());
        }
        public void RestartGame()
        {
            hudController.SetHighScore(scoreController.GetHighScore());
            hudController.ResetData();
            hudController.StartGameTimer(gameTime, OnGameTimerFinished);

            scoreController.ResetScore();
            orderController.InitializeWindows();
        }

        private void SetupKitchenOperators()
        {
            var fridge = Instantiate(frdgePrefab, kitchenItemsParent);
            fridge.gameObject.transform.localPosition = new Vector3(-1.2f, -2.1f, 9.3f);

            var chopper = Instantiate(oneSlotChopperPrefab, kitchenItemsParent);
            chopper.gameObject.transform.localPosition = new Vector3(-2, -3, 0);

            var stove = Instantiate(twoSlotStovePrefab, kitchenItemsParent);
            stove.gameObject.transform.localPosition = new Vector3(2, -3, 0);

            var trashCan = Instantiate(trashCanPrefab, kitchenItemsParent);
            trashCan.gameObject.transform.localPosition = new Vector3(4.4f, -3, 9.5f);
        }

        private void InitializeWindows()
        {
            float xTransform = -4f;
            for (int i = 0; i < 4; i++)
            {
                var windowObject = Instantiate(WindowPrefab, kitchenItemsParent);
                windowObject.transform.localPosition = new Vector3(xTransform, -1.6f, -9.5f);
                windowControllers.Add(windowObject.GetComponent<WindowController>());
                xTransform += 3f;
            }
        }

        public void OnGameTimerFinished()
        {
            bool isNewHighScore = scoreController.IsNewHighScore();
            if (isNewHighScore)
                scoreController.SaveHighScore();
            OpenGameOverPopup(isNewHighScore);
        }

        private void OpenGameOverPopup(bool isNewHighScore)
        {
            var totalScore = scoreController.GetTotalScore();
            var gameOverPopupPrefab = Resources.Load("GameOverPopup") as GameObject;
            var popup = Instantiate(gameOverPopupPrefab, transform);
            popup.GetComponent<GameOverPopup>().Setup(totalScore,
                onReplayButtonPressed: () =>
                {
                    Destroy(popup);
                    RestartGame();
                },
                onExitButtonPressed: () =>
                {
                    Destroy(popup);
                    ServiceLocator.Find<GameManager>().ChangeSceneToMenu();
                },
                isNewHighScore);
        }




    }
}

