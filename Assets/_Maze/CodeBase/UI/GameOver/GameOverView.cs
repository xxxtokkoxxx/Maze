using System;
using TMPro;
using UnityEngine;

namespace _Maze.CodeBase.UI.GameOver
{
    public class GameOverView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _totalTimeText;
        [SerializeField] private TextMeshProUGUI _totalStepsText;

        private GameOverUICallbacks _callbacks;
        public override ViewType ViewType => ViewType.GameOver;

        public void Initialize(GameOverUICallbacks callbacks)
        {
            _callbacks = callbacks;
        }

        public void GoToMainMenu()
        {
            _callbacks.GoToMainMenu();
        }

        public void RestartGame()
        {
            _callbacks.RestartGame();
        }

        public void SetGameResultsText(float totalTime, int totalSteps)
        {
            Debug.Log(totalTime);
            int minutes = Mathf.FloorToInt(totalTime / 60f);
            int seconds = Mathf.FloorToInt(totalTime % 60f);

            _totalTimeText.text = $"{minutes:00}:{seconds:00}";
            _totalStepsText.text = totalSteps.ToString();
        }
    }
}