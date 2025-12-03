using System;
using TMPro;
using UnityEngine;

namespace _Maze.CodeBase.UI.Hud
{
    public class HeadsUpDisplayView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _stepsCount;

        public override ViewType ViewType => ViewType.Hud;

        public void SetStepsCount(int stepsCount)
        {
            _stepsCount.text = stepsCount.ToString();
        }

        public void SetSessionTime(TimeSpan timeSpan)
        {
            _stepsCount.text = timeSpan.ToString();
        }
    }
}