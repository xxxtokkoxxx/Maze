using System;
using _Maze.CodeBase.Data;

namespace _Maze.CodeBase.Progress
{
    public interface IGameRuntimeData
    {
        void SetData(GameProgressData gameProgressData);
        int AddPlayerStepsCount(int playerStepsCount);
        void SetStepsCount(int playerStepsCount);
        void SetSessionTime(float sessionTime);
        void SetSeed(int seed);
        void SetMazeData(MazeData mazeData);
        public float GetSessionTime();
        public int GetPlayerStepsCount();
    }
}