using _Maze.CodeBase.GamePlay.GameSession;
using _Maze.CodeBase.Progress;

namespace _Maze.CodeBase.UI.GameOver
{
    public class GameOverUiController : BaseUiController<GameOverView>, IViewController
    {
        private bool _subscribed;
        private GameOverUICallbacks _callbacks;

        private readonly IGameSessionRunner _gameSessionRunner;
        private readonly IGameRuntimeDataContainer _gameRuntimeDataContainer;
        private readonly IUIService _uiService;
        private readonly IUIViewsFactory _viewsFactory;

        public override ViewType ViewType => ViewType.GameOver;

         public GameOverUiController(IUIViewsFactory viewsFactory,
            IGameSessionRunner gameSessionRunner,
            IGameRuntimeDataContainer gameRuntimeDataContainer,
            IUIService uiService)
        {
            _viewsFactory = viewsFactory;
            _gameSessionRunner = gameSessionRunner;
            _gameRuntimeDataContainer = gameRuntimeDataContainer;
            _uiService = uiService;
        }

        public override void Show()
        {
            Subscribe();

            if (View == null)
            {
                View = _viewsFactory.CreateView<GameOverView>(ViewType.GameOver);
                View.Initialize(_callbacks);
            }

            View.SetGameResultsText(_gameRuntimeDataContainer.GetSessionTime(), _gameRuntimeDataContainer.GetPlayerStepsCount());
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
            _uiService.HideWindow(ViewType);
            _gameSessionRunner.RestartGame();
        }

        private void GoToMainMenu()
        {
            _gameSessionRunner.EndGame();

            _uiService.HideWindow(ViewType.Hud);
            _uiService.HideWindow(ViewType.GameOver);
            _uiService.ShowWindow(ViewType.MainMenu);
        }
    }
}