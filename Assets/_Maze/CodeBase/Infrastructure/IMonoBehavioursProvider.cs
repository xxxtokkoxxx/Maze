using UnityEngine;
using VContainer.Unity;

namespace _Maze.CodeBase.Infrastructure
{
    public interface IMonoBehavioursProvider
    {
        Transform MazeSpawnPoint { get; }
        Camera CachedCamera { get; }
        Transform UISpawnPoint { get; }
        Transform HUDSpawnPoint { get; }
        LifetimeScope LifetimeScope { get; }
    }
}