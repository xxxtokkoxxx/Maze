using System;

namespace _Maze.CodeBase.Data
{
    [Serializable]
    public class PlayerProgressData
    {
        public int StepsCount { get; set; }
        public float SessionTime { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}