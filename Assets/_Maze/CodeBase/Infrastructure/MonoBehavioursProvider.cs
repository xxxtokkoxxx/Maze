using UnityEngine;

namespace _Maze.CodeBase.Infrastructure
{
    public class MonoBehavioursProvider : MonoBehaviour, IMonoBehavioursProvider
    {
        [SerializeField] private Transform _mazeSpawnPoint;
        [SerializeField] private Transform _cameraTransform;
        public Transform MazeSpawnPoint => _mazeSpawnPoint;

        public Transform CameraTransform => _cameraTransform;
    }
}