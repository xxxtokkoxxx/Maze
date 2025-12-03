using _Maze.CodeBase.GamePlay.GameSession;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.Input;
using UnityEngine;
using VContainer;

namespace _Maze.CodeBase.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        public float CellSize;
        public int Width;
        public int Height;
        public int ExistsCount;

        private IInputStateProvider _inputStateProvider;
        private IGameSessionRunner _gameSessionRunner;
        //
        // [Inject]
        // public void Inject(IGameSessionRunner gameSessionRunner, IInputStateProvider inputStateProvider)
        // {
        //     _inputStateProvider = inputStateProvider;
        //     _gameSessionRunner = gameSessionRunner;
        // }
        //
        [Inject]
        public void Inject(IGameSessionRunner gameSessionRunner, IInputStateProvider inputStateProvider)
        {
            _inputStateProvider = inputStateProvider;
            _gameSessionRunner = gameSessionRunner;
        }

        private void Start()
        {
            _inputStateProvider.Initialize();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                _gameSessionRunner.StartGame(new MazeConfiguration(Width, Height, CellSize, ExistsCount));
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
            {
                _gameSessionRunner.RestartGame();
            }
        }
    }
}