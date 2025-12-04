using System;
using _Maze.CodeBase.Configuration;
using _Maze.CodeBase.Data;
using _Maze.CodeBase.GamePlay.GameSession;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.Progress;

namespace _Maze.CodeBase.UI.MainMenu
{
    public class MainMenuUIController : BaseUiController<MainMenuView>, IViewController
    {
        private bool _subscribed;

        private MainMenuCallbacks _callbacks;
        private MazeData _mazeData;

        private readonly IUIViewsFactory _viewsFactory;
        private readonly IGameSessionRunner _gameSessionRunner;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IUIService _uiService;
        private readonly IGameRuntimeDataContainer _gameRuntimeDataContainer;
        private readonly IGameConfiguration _gameConfiguration;

        public MainMenuUIController(IUIViewsFactory viewsFactory,
            IGameSessionRunner gameSessionRunner,
            ISaveLoadService saveLoadService,
            IUIService uiService,
            IGameRuntimeDataContainer gameRuntimeDataContainer,
            IGameConfiguration gameConfiguration)
        {
            _viewsFactory = viewsFactory;
            _gameSessionRunner = gameSessionRunner;
            _saveLoadService = saveLoadService;
            _uiService = uiService;
            _gameRuntimeDataContainer = gameRuntimeDataContainer;
            _gameConfiguration = gameConfiguration;
        }

        public override ViewType ViewType => ViewType.MainMenu;

        public override void Show()
        {
            if (_mazeData == null)
            {
                _mazeData = new MazeData(_gameConfiguration.MinMazeSize.x, _gameConfiguration.MinMazeSize.y, 1,
                    _gameConfiguration.MinExists);
            }

            Subscribe();

            if (View == null)
            {
                View = _viewsFactory.CreateView<MainMenuView>(ViewType.MainMenu);
                View.Initialize(_callbacks);
            }

            View.UpdateExistsCountText(_mazeData.ExitsCount);
            View.UpdateMazeWidthText(_mazeData.Width);
            View.UpdateMazeHeightText(_mazeData.Height);

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
            GameProgressData data = GetGameProgressData();
            _gameRuntimeDataContainer.SetData(data);
            data.MazeData.Seed = SeedGenerator.GenerateSeed();

            _gameSessionRunner.StartGame(data);
            _uiService.HideWindow(ViewType);
        }

        private void LoadGame()
        {
            GameProgressData data = _saveLoadService.LoadGame();
            _gameRuntimeDataContainer.SetData(data);

            if (data != null)
            {
                _gameSessionRunner.StartGame(data, true);
            }

            _uiService.HideWindow(ViewType);
        }

        private void SetExistsCount(int existsCount)
        {
            _mazeData.ExitsCount = Math.Clamp(_mazeData.ExitsCount + existsCount, _gameConfiguration.MinExists,
                _gameConfiguration.MaxExists);

            View.UpdateExistsCountText(_mazeData.ExitsCount);
        }

        private void SetMazeWidth(int mazeWidth)
        {
            _mazeData.Width = Math.Clamp(_mazeData.Width + mazeWidth, _gameConfiguration.MinMazeSize.x,
                _gameConfiguration.MaxMazeSize.x);

            View.UpdateMazeWidthText(_mazeData.Width);
        }

        private void SetMazeHeight(int mazeHeight)
        {
            _mazeData.Height = Math.Clamp(_mazeData.Height + mazeHeight, _gameConfiguration.MinMazeSize.y,
                _gameConfiguration.MaxMazeSize.y);

            View.UpdateMazeHeightText(_mazeData.Height);
        }

        private void SetLoadGameButtonEnabled()
        {
            bool isEnabled = _saveLoadService.SaveExists();
            View.SetLoadButtonEnabled(isEnabled);
        }

        private GameProgressData GetGameProgressData()
        {
            GameProgressData data = new GameProgressData(1, new PlayerProgressData(), _mazeData);
            return data;
        }
    }
}