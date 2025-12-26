using _Maze.CodeBase.Infrastructure.StateMachine;
using _Maze.CodeBase.Progress;
using _Maze.CodeBase.Scenes;

namespace _Maze.CodeBase.UI.MainMenu
{
    public class MainMenuUIController : BaseUiController<MainMenuView>
    {
        private bool _subscribed;

        private MainMenuCallbacks _callbacks;

        private readonly IUIViewsFactory _viewsFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IGameStateMachine _gameStateMachine;

        public MainMenuUIController(IUIViewsFactory viewsFactory,
            ISaveLoadService saveLoadService,
            IUIService uiService,
            ISceneLoaderService sceneLoaderService,
            IGameStateMachine gameStateMachine) : base(uiService)
        {
            _viewsFactory = viewsFactory;
            _saveLoadService = saveLoadService;
            _sceneLoaderService = sceneLoaderService;
            _gameStateMachine = gameStateMachine;
        }

        public override ViewType ViewType => ViewType.MainMenu;

        public override void Show()
        {
            Subscribe();

            if (View == null)
            {
                View = _viewsFactory.CreateView<MainMenuView>(ViewType.MainMenu);
                View.Initialize(_callbacks);
            }

            SetLoadGameButtonEnabled();
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
                _callbacks = new MainMenuCallbacks();
            }

            _callbacks.OnStartGame += StartGame;
            _callbacks.OnLoadGame += LoadGame;
        }

        private void Unsubscribe()
        {
            _callbacks.OnStartGame -= StartGame;
            _callbacks.OnLoadGame -= LoadGame;

            _subscribed = false;
        }

        private async void StartGame()
        {
            await _sceneLoaderService.LoadScene("Level_1");
            
            UIService.HideWindow(ViewType);
        }

        private void LoadGame()
        {
            UIService.HideWindow(ViewType);
        }

        private void SetLoadGameButtonEnabled()
        {
            bool isEnabled = _saveLoadService.SaveExists();
            View.SetLoadButtonEnabled(isEnabled);
        }
    }
}