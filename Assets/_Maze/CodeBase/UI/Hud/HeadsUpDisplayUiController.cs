using _Maze.CodeBase.Infrastructure;
using _Maze.CodeBase.Progress;
using UnityEngine;

namespace _Maze.CodeBase.UI.Hud
{
    public class HeadsUpDisplayUiController : BaseUiController<HeadsUpDisplayView>, IHeadsUpDisplay
    {
        private readonly IUIViewsFactory _viewsFactory;
        private readonly IGameRuntimeDataContainer _gameRuntimeDataContainer;
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;

        public HeadsUpDisplayUiController(IUIViewsFactory uiViewsFactory,
            IGameRuntimeDataContainer gameRuntimeDataContainer, IMonoBehavioursProvider monoBehavioursProvider)
        {
            _viewsFactory = uiViewsFactory;
            _gameRuntimeDataContainer = gameRuntimeDataContainer;
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

            UpdateStepsCount(_gameRuntimeDataContainer.GetPlayerStepsCount());
            UpdateTimer(_gameRuntimeDataContainer.GetSessionTime());
        }

        public override void Hide()
        {
            _viewsFactory.DestroyView(View.Id);
        }

        public void UpdateStepsCount(int stepsCount)
        {
            View.SetStepsCount(_gameRuntimeDataContainer.GetPlayerStepsCount());
        }

        public void UpdateTimer(float sessionTime)
        {
            int minutes = Mathf.FloorToInt(sessionTime / 60f);
            int seconds = Mathf.FloorToInt(sessionTime % 60f);

            View.SetSessionTime(minutes, seconds);
        }
    }
}