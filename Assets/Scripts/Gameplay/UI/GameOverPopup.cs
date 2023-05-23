using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class GameOverPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI newHighScoreText;
        [SerializeField] Button replayButton;
        [SerializeField] Button exitButton;
        public void Setup(int score, Action onReplayButtonPressed, Action onExitButtonPressed, bool isNewHighScore = false)
        {
            scoreText.text = "Score: " + score.ToString();
            if (isNewHighScore) newHighScoreText.gameObject.SetActive(true);
            replayButton.onClick.AddListener(onReplayButtonPressed.Invoke);
            exitButton.onClick.AddListener(onExitButtonPressed.Invoke);
            this.gameObject.SetActive(true);

        }
    }
}

