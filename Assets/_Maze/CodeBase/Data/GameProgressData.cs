using System;

namespace _Maze.CodeBase.Data
{
    [Serializable]
    public class GameProgressData
    {
        public int Seed { get; set; }
        public PlayerProgressData PlayerProgress { get; set; }
        public MazeData MazeData { get; set; }

        public GameProgressData(int seed, PlayerProgressData playerProgress, MazeData mazeData)
        {
            Seed = seed;
            PlayerProgress = playerProgress;
            MazeData = mazeData;
        }
    }
}