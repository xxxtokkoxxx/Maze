using _Maze.CodeBase.Scenes;
using _Maze.CodeBase.UI;
using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public class GameMenuState : BaseState
    {
        private readonly IUIService _uiService;
        private readonly ISceneLoaderService _sceneLoaderService;

        public GameMenuState(IGameStateMachine gameStateMachine, IUIService uiService, ISceneLoaderService sceneLoaderService) : base(gameStateMachine)
        {
            _uiService = uiService;
            _sceneLoaderService = sceneLoaderService;
        }

        public override async UniTask Enter(object payload = null)
        {
            await _sceneLoaderService.LoadScene(SceneNames.GameMenu);
            _uiService.ShowWindow(ViewType.MainMenu);
        }
    }
}