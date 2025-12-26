using _Maze.CodeBase.UI;
using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : BaseState
    {
        private readonly IUIViewsFactory _uiViewsFactory;

        public BootstrapState(IGameStateMachine gameStateMachine, IUIViewsFactory uiViewsFactory) : base(gameStateMachine)
        {
            _uiViewsFactory = uiViewsFactory;
        }

        public override async UniTask Enter()
        {
            await _uiViewsFactory.LoadViews();
            await GameStateMachine.Enter<GameMenuState>();
        }
    }
}