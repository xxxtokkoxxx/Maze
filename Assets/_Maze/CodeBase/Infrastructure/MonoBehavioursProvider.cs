using UnityEngine;

namespace _Maze.CodeBase.Infrastructure
{
    public class MonoBehavioursProvider : MonoBehaviour, IMonoBehavioursProvider
    {
        [SerializeField] private Transform _mazeSpawnPoint;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _uiSpawnPoint;
        [SerializeField] private Transform _hudSpawnPoint;
        public Transform MazeSpawnPoint => _mazeSpawnPoint;
        public Transform CameraTransform => _cameraTransform;
        public Transform UISpawnPoint => _uiSpawnPoint;
        public Transform HUDSpawnPoint => _hudSpawnPoint;
    }
}