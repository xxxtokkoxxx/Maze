using UnityEngine;

namespace _Maze.CodeBase.Infrastructure
{
    public interface IMonoBehavioursProvider
    {
        Transform MazeSpawnPoint { get; }
    }
}