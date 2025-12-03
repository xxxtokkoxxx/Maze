using UnityEngine;

namespace _Maze.CodeBase.Infrastructure
{
    public interface IMonoBehavioursProvider
    {
        Transform MazeSpawnPoint { get; }
        Transform CameraTransform { get; }
        Transform UISpawnPoint { get; }
        Transform HUDSpawnPoint { get; }
    }
}