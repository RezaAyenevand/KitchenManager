using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Score
{
    public class ScoreController
    {
        private int totalScore;
        private Action<int> updateScoreCallback;
        public ScoreController(Action<int> updateScoreCallback)
        {
            this.updateScoreCallback = updateScoreCallback;
            ResetScore();
            SetupPlayerPrefs();
        }

        private void SetupPlayerPrefs()
        {
            if (!PlayerPrefs.HasKey("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", 0);
                PlayerPrefs.Save();
            }
        }

        public int GetHighScore() => PlayerPrefs.GetInt("HighScore");

        public void ResetScore()
        {
            totalScore = 0;
        }

        public int CalculateScore(Gameplay.Order.Order order)
        {
            int score = 0;
            int maxScore = 0;
            foreach (var ingredientStatus in order.GetIngredientDeliveryStatusList())
                maxScore += ingredientStatus.ingredientScore.score;

            int spentTime = (int)(order.GetEndTime() - order.GetStartTime()).TotalSeconds;
            score += (maxScore - spentTime);
            totalScore += score;
            updateScoreCallback.Invoke(totalScore);
            return score;
        }

        public int GetTotalScore() => totalScore;

        public bool IsNewHighScore()
        {
            return GetTotalScore() > PlayerPrefs.GetInt("HighScore");
        }

        public void SaveHighScore()
        {
            if (PlayerPrefs.HasKey("HighScore"))
            {
                var highScore = PlayerPrefs.GetInt("HighScore");
                if (GetTotalScore() > highScore)
                    PlayerPrefs.SetInt("HighScore", GetTotalScore());
            }
            else
            {
                PlayerPrefs.SetInt("HighScore", GetTotalScore());
            }
            PlayerPrefs.Save();
        }
    }
}

