using _Maze.CodeBase.Data;

namespace _Maze.CodeBase.Progress
{
    public class GameRuntimeDataContainer : IGameRuntimeData
    {
        private GameProgressData _gameProgressData;

        public void SetData(GameProgressData gameProgressData)
        {
            _gameProgressData = gameProgressData;
        }

        public int AddPlayerStepsCount(int playerStepsCount)
        {
            _gameProgressData.PlayerProgress.StepsCount += playerStepsCount;
            return _gameProgressData.PlayerProgress.StepsCount;
        }

        public void SetStepsCount(int playerStepsCount)
        {
            _gameProgressData.PlayerProgress.StepsCount = playerStepsCount;
        }

        public void SetSessionTime(float sessionTime)
        {
            _gameProgressData.PlayerProgress.SessionTime = sessionTime;
        }

        public void SetSeed(int seed)
        {
        }

        public void SetMazeData(MazeData mazeData)
        {
            _gameProgressData.MazeData = mazeData;
        }

        public float GetSessionTime()
        {
            return _gameProgressData.PlayerProgress.SessionTime;
        }

        public int GetPlayerStepsCount()
        {
            return _gameProgressData.PlayerProgress.StepsCount;
        }
    }
}