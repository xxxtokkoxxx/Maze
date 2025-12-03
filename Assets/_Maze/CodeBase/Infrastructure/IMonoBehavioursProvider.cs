using UnityEngine;

namespace _Maze.CodeBase.Infrastructure
{
    public interface IMonoBehavioursProvider
    {
        Transform MazeSpawnPoint { get; }
        Camera CachedCamera { get; }
        Transform UISpawnPoint { get; }
        Transform HUDSpawnPoint { get; }
    }
}