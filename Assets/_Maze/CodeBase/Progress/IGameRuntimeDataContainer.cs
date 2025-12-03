using _Maze.CodeBase.Data;
using UnityEngine;

namespace _Maze.CodeBase.Progress
{
    public interface IGameRuntimeDataContainer
    {
        void SetData(GameProgressData gameProgressData);
        int AddPlayerStepsCount(int playerStepsCount);
        void SetStepsCount(int playerStepsCount);
        void SetSessionTime(float sessionTime);
        void SetSeed(int seed);
        void SetMazeData(MazeData mazeData);
        public float GetSessionTime();
        public int GetPlayerStepsCount();
        GameProgressData GetGameProgressData();
        void SetPlayerPosition(Vector2Int currentPosition);
    }
}