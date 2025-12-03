using _Maze.CodeBase.GamePlay.Maze;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public interface IGameSessionRunner
    {
        void StartGame(MazeConfiguration mazeConfiguration);
        void RestartGame();
        void EndGame();
    }
}