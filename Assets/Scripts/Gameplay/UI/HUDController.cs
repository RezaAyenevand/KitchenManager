using Infrastructure.ServiceLocating;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI remainingTimeLeft;
        [SerializeField] TextMeshProUGUI highScoreText;
        [SerializeField] GameObject pauseButton;
        [SerializeField] GameObject unPauseButton;

        // Start is called before the first frame update
        void Start()
        {
            pauseButton.SetActive(true);
            unPauseButton.SetActive(false);
            ResetData();
        }
        public void SetHighScore(int score)
        {
            highScoreText.text = "HighScore: " + score;
        }
        public void ResetData()
        {
            scoreText.text = "Score: " + 0;
            remainingTimeLeft.text = "Time Left: " + 0;
        }
        public void UpdateScore(int score)
        {
            scoreText.text = "Score: " + score.ToString();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            pauseButton.SetActive(false);
            unPauseButton.SetActive(true);
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;
            pauseButton.SetActive(true);
            unPauseButton.SetActive(false);
        }

        public void ExitGame()
        {
            ServiceLocator.Find<GameManager>().ChangeSceneToMenu();
        }

        public void StartGameTimer(int time, Action onComplete)
        {
            StartCoroutine(GameTimer(time, onComplete));
        }

        private IEnumerator GameTimer(int time, Action onComplete)
        {
            for (int i = time; i >= 0; i--)
            {
                yield return new WaitForSeconds(1);
                remainingTimeLeft.text = "Time Left: " + i;

            }

            onComplete.Invoke();
        }
    }

}
