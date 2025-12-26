using _Maze.CodeBase.UI;
using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public class GameMenuState : BaseState
    {
        private readonly IUIService _uiService;

        public GameMenuState(IGameStateMachine gameStateMachine, IUIService uiService) : base(gameStateMachine)
        {
            _uiService = uiService;
        }

        public override UniTask Enter()
        {
            _uiService.ShowWindow(ViewType.MainMenu);
            return UniTask.CompletedTask;
        }
    }
}