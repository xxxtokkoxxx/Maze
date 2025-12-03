using UnityEngine;

namespace _Maze.CodeBase.Infrastructure
{
    public class MonoBehavioursProvider : MonoBehaviour, IMonoBehavioursProvider
    {
        [SerializeField] private Transform _mazeSpawnPoint;
        [SerializeField] private Camera _cachedCamera;
        [SerializeField] private Transform _uiSpawnPoint;
        [SerializeField] private Transform _hudSpawnPoint;
        public Transform MazeSpawnPoint => _mazeSpawnPoint;
        public Camera CachedCamera => _cachedCamera;
        public Transform UISpawnPoint => _uiSpawnPoint;
        public Transform HUDSpawnPoint => _hudSpawnPoint;
    }
}