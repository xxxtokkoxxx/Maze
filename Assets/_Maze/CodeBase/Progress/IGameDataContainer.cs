using System;
using _Maze.CodeBase.Data;

namespace _Maze.CodeBase.Progress
{
    public interface IGameDataContainer
    {
        void SetData(GameProgressData gameProgressData);
        void AddPlayerStepsCount(int playerStepsCount);
        void SetSessionTime(TimeSpan sessionTime);
        void SetSeed(int seed);
        void SetMazeData(MazeData mazeData);
    }
}