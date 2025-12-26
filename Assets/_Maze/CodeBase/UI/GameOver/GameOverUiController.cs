using _Maze.CodeBase.GamePlay.GameSession;

namespace _Maze.CodeBase.UI.GameOver
{
    public class GameOverUiController : BaseUiController<GameOverView>
    {
        private bool _subscribed;
        private GameOverUICallbacks _callbacks;

        private readonly IGameSessionRunner _gameSessionRunner;
        private readonly IUIViewsFactory _viewsFactory;

        public override ViewType ViewType => ViewType.GameOver;

         public GameOverUiController(IUIViewsFactory viewsFactory,
            IGameSessionRunner gameSessionRunner,
            IUIService uiService) : base(uiService)
         {
            _viewsFactory = viewsFactory;
            _gameSessionRunner = gameSessionRunner;
        }

        public override void Show()
        {
            Subscribe();

            if (View == null)
            {
                View = _viewsFactory.CreateView<GameOverView>(ViewType.GameOver);
                View.Initialize(_callbacks);
            }
        }

        public override void Hide()
        {
            Unsubscribe();
            _viewsFactory.DestroyView(View.Id);
        }

        private void Subscribe()
        {
            if (_subscribed)
            {
                return;
            }

            _subscribed = true;

            if (_callbacks == null)
            {
                _callbacks = new GameOverUICallbacks();
            }

            _callbacks.OnRestartGame += RestartGame;
            _callbacks.OnGoToMainMenu += GoToMainMenu;
        }

        private void Unsubscribe()
        {
            _callbacks.OnRestartGame -= RestartGame;
            _callbacks.OnGoToMainMenu -= GoToMainMenu;

            _subscribed = false;
        }

        private void RestartGame()
        {
            UIService.HideWindow(ViewType);
            _gameSessionRunner.RestartGame();
        }

        private void GoToMainMenu()
        {
            _gameSessionRunner.EndGame();

            UIService.HideWindow(ViewType.Hud);
            UIService.HideWindow(ViewType.GameOver);
            UIService.ShowWindow(ViewType.MainMenu);
        }
    }
}