using System;

namespace _Maze.CodeBase.UI.Hud
{
    public class HeadsUpDisplayUiController : BaseUiController<HeadsUpDisplayView>, IHeadsUpDisplay
    {
        private readonly IUIViewsFactory _viewsFactory;

        public HeadsUpDisplayUiController(IUIViewsFactory uiViewsFactory)
        {
            _viewsFactory = uiViewsFactory;
        }

        public override ViewType ViewType => ViewType.Hud;

        public override void Show()
        {
            if (View == null)
            {
                View = _viewsFactory.CreateView<HeadsUpDisplayView>(ViewType.MainMenu);
            }
        }

        public override void Hide()
        {
            _viewsFactory.DestroyView(View.Id);
        }

        public void UpdateStepsCount(int stepsCount)
        {
            View.SetStepsCount(stepsCount);
        }

        public void UpdateTimer(TimeSpan timeSpan)
        {
            View.SetSessionTime(timeSpan);
        }
    }
}