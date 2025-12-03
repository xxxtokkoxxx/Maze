using _Maze.CodeBase.GamePlay.GameSession;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.Progress;
using _Maze.CodeBase.UI.GameOver;

namespace _Maze.CodeBase.UI.Pause
{
    public class PauseUIController : BaseUiController<PauseView>
    {
        private bool _subscribed;
        private PauseUICallbacks _callbacks;

        private readonly IGameSessionRunner _gameSessionRunner;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGamePauseProcessor _gamePauseProcessor;
        private readonly IUIViewsFactory _viewsFactory;

        public override ViewType ViewType => ViewType.GameOver;

        public PauseUIController(IUIViewsFactory viewsFactory,
            IGameSessionRunner gameSessionRunner,
            ISaveLoadService saveLoadService,
            IGamePauseProcessor gamePauseProcessor)
        {
            _viewsFactory = viewsFactory;
            _gameSessionRunner = gameSessionRunner;
            _saveLoadService = saveLoadService;
            _gamePauseProcessor = gamePauseProcessor;
        }

        public override void Show()
        {
            Subscribe();

            if (View == null)
            {
                View = _viewsFactory.CreateView<PauseView>(ViewType.GameOver);
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
                _callbacks = new PauseUICallbacks();
            }

            _callbacks.OnRestartGame += RestartGame;
            _callbacks.OnGoToMainMenu += GoToMainMenu;
            _callbacks.OnSave += SaveGame;
            _callbacks.OnResumeGame += ResumeGame;
        }

        private void Unsubscribe()
        {
            _callbacks.OnRestartGame -= RestartGame;
            _callbacks.OnGoToMainMenu -= GoToMainMenu;
            _callbacks.OnSave -= SaveGame;
            _callbacks.OnResumeGame -= ResumeGame;

            _subscribed = false;
        }

        private void SaveGame()
        {
            _saveLoadService.SaveGame(null);
        }

        private void ResumeGame()
        {
            _gamePauseProcessor.SetPaused(false);
        }

        private void RestartGame()
        {
            _gameSessionRunner.RestartGame();
            _gamePauseProcessor.SetPaused(false);
        }

        private void GoToMainMenu()
        {
            _gameSessionRunner.EndGame();
            _gamePauseProcessor.SetPaused(false);
        }
    }
}