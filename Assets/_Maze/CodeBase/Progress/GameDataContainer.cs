using System;
using _Maze.CodeBase.Data;

namespace _Maze.CodeBase.Progress
{
    public class GameDataContainer : IGameDataContainer
    {
        private GameProgressData _gameProgressData;

        public void SetData(GameProgressData gameProgressData)
        {
            _gameProgressData = gameProgressData;
        }

        public void AddPlayerStepsCount(int playerStepsCount)
        {
            _gameProgressData.PlayerProgress.StepsCount += playerStepsCount;
        }

        public void SetSessionTime(TimeSpan sessionTime)
        {
            _gameProgressData.PlayerProgress.SessionTime += sessionTime;
        }

        public void SetSeed(int seed)
        {

        }

        public void SetMazeData(MazeData mazeData)
        {
            _gameProgressData.MazeData = mazeData;
        }
    }
}