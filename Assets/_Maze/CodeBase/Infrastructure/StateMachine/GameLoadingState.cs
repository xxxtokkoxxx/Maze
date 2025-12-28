using _Maze.CodeBase.Infrastructure.DI;
using _Maze.CodeBase.LayersAndTahs;
using _Maze.CodeBase.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.Infrastructure.StateMachine
{
    public class GameLoadingState : BaseState
    {
        private readonly ISceneLoaderService _sceneLoaderService;

        public GameLoadingState(IGameStateMachine gameStateMachine,
            ISceneLoaderService sceneLoaderService) : base(gameStateMachine)
        {
            _sceneLoaderService = sceneLoaderService;
        }

        public override async UniTask Enter(object payload = null)
        {
            if (payload is string str)
            {
                await _sceneLoaderService.LoadScene(str);
            }
            else
            {
                await _sceneLoaderService.LoadScene(SceneNames.Level_1);
            }

            GameScope gameScope = GameObject.FindGameObjectWithTag(Tags.GameScope).GetComponent<GameScope>();
            gameScope.Build();

            await GameStateMachine.Enter<GameLoopState>();
        }
    }
}