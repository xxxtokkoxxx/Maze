using _Maze.CodeBase.Data;
using _Maze.CodeBase.GamePlay.Maze;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public interface IGameSessionRunner
    {
        void StartGame(MazeData mazeData);
        void RestartGame();
        void EndGame();
    }
}