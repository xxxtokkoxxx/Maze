using _Maze.CodeBase.Data;
using _Maze.CodeBase.GamePlay.GameSession;
using _Maze.CodeBase.Progress;

namespace _Maze.CodeBase.UI.MainMenu
{
    public class MainMenuUIController : BaseUiController<MainMenuView>, IViewController
    {
        private bool _subscribed;

        private MainMenuCallbacks _callbacks;
        private MazeData _data;

        private readonly IUIViewsFactory _viewsFactory;
        private readonly IGameSessionRunner _gameSessionRunner;
        private readonly ISaveLoadService _saveLoadService;

        public MainMenuUIController(IUIViewsFactory viewsFactory,
            IGameSessionRunner gameSessionRunner,
            ISaveLoadService saveLoadService)
        {
            _viewsFactory = viewsFactory;
            _gameSessionRunner = gameSessionRunner;
            _saveLoadService = saveLoadService;
        }

        public override ViewType ViewType => ViewType.MainMenu;

        public override void Show()
        {
            if (_data == null)
            {
                _data = new MazeData(10, 10, 1, 1);
            }

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
            _callbacks.OnSetMazeWidth += SetMazeWidth;
            _callbacks.OnSetMazeHeight += SetMazeHeight;
            _callbacks.OnSetExistsCount += SetExistsCount;
        }

        private void Unsubscribe()
        {
            _callbacks.OnStartGame -= StartGame;
            _callbacks.OnLoadGame -= LoadGame;
            _callbacks.OnSetMazeWidth -= SetMazeWidth;
            _callbacks.OnSetMazeHeight -= SetMazeHeight;
            _callbacks.OnSetExistsCount -= SetExistsCount;

            _subscribed = false;
        }

        private void StartGame()
        {
            _gameSessionRunner.StartGame(_data);
        }

        private void LoadGame()
        {
            GameProgressData data = _saveLoadService.LoadGame();

            if (data != null)
            {
                //TODO: replace parameter later
                _gameSessionRunner.StartGame(data.MazeData);
            }
        }

        private void SetExistsCount(int existsCount)
        {
            View.UpdateExistsCountText(existsCount);
            _data.ExitsCount = existsCount;
        }

        private void SetMazeWidth(int mazeWidth)
        {
            View.UpdateMazeWidthText(mazeWidth);
            _data.Width = mazeWidth;
        }

        private void SetMazeHeight(int mazeHeight)
        {
            View.UpdateMazeHeightText(mazeHeight);
            _data.Height = mazeHeight;
        }

        private void SetLoadGameButtonEnabled()
        {
            bool isEnabled = _saveLoadService.SaveExists();
            View.SetLoadButtonEnabled(isEnabled);
        }
    }
}