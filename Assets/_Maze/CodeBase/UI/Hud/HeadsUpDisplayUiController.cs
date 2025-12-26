using _Maze.CodeBase.Infrastructure;
using UnityEngine;

namespace _Maze.CodeBase.UI.Hud
{
    public class HeadsUpDisplayUiController : BaseUiController<HeadsUpDisplayView>, IHeadsUpDisplay
    {
        private readonly IUIViewsFactory _viewsFactory;
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;

        public HeadsUpDisplayUiController(IUIViewsFactory uiViewsFactory, IMonoBehavioursProvider monoBehavioursProvider, IUIService uiService) : base(uiService)
        {
            _viewsFactory = uiViewsFactory;
            _monoBehavioursProvider = monoBehavioursProvider;
        }

        public override ViewType ViewType => ViewType.Hud;

        public override void Show()
        {
            if (View == null)
            {
                View = _viewsFactory.CreateView<HeadsUpDisplayView>(ViewType.Hud,
                    _monoBehavioursProvider.HUDSpawnPoint);
            }
        }

        public override void Hide()
        {
            _viewsFactory.DestroyView(View.Id);
        }

        public void UpdateStepsCount(int stepsCount)
        {

        }

        public void UpdateTimer(float sessionTime)
        {
            int minutes = Mathf.FloorToInt(sessionTime / 60f);
            int seconds = Mathf.FloorToInt(sessionTime % 60f);

            View.SetSessionTime(minutes, seconds);
        }
    }
}