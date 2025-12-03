using TMPro;
using UnityEngine;

namespace _Maze.CodeBase.UI.Hud
{
    public class HeadsUpDisplayView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _stepsCountText;
        [SerializeField] private TextMeshProUGUI _sessionTimeText;

        public override ViewType ViewType => ViewType.Hud;

        public void SetStepsCount(int stepsCount)
        {
            _stepsCountText.text = stepsCount.ToString();
        }

        public void SetSessionTime(int minutes, int seconds)
        {
            _sessionTimeText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}