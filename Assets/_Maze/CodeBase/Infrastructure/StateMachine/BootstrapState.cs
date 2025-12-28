using _Maze.CodeBase.UI;
using _Maze.CodeBase.Utilities;
using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : BaseState
    {
        private readonly IUIViewsFactory _uiViewsFactory;

        public BootstrapState(IGameStateMachine gameStateMachine, IUIViewsFactory uiViewsFactory) : base(
            gameStateMachine)
        {
            _uiViewsFactory = uiViewsFactory;
        }

#if UNITY_EDITOR
        public override async UniTask Enter(object payload = null)
        {
            await _uiViewsFactory.LoadViews();
            if (!string.IsNullOrEmpty(GameRunnerFromAnyScene.GameSceneName))
            {
                await GameStateMachine.Enter<GameLoadingState>(GameRunnerFromAnyScene.GameSceneName);
            }
            else
            {
                await GameStateMachine.Enter<GameMenuState>();
            }
        }

#else
        public override async UniTask Enter()
        {
            await _uiViewsFactory.LoadViews();
            await GameStateMachine.Enter<GameMenuState>();
        }
#endif
    }
}