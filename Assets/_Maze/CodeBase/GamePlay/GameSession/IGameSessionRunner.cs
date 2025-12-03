using _Maze.CodeBase.Data;

namespace _Maze.CodeBase.GamePlay.GameSession
{
    public interface IGameSessionRunner
    {
        void StartGame(GameProgressData data, bool loadGameProgressData = false);
        void RestartGame();
        void EndGame();
    }
}