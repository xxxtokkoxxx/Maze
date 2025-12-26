using UnityEngine;
using VContainer.Unity;

namespace _Maze.CodeBase.Infrastructure
{
    public class MonoBehavioursProvider : MonoBehaviour, IMonoBehavioursProvider
    {
        [SerializeField] private Transform _mazeSpawnPoint;
        [SerializeField] private Camera _cachedCamera;
        [SerializeField] private Transform _uiSpawnPoint;
        [SerializeField] private Transform _hudSpawnPoint;
        [SerializeField] private LifetimeScope _lifetimeScope;

        public Transform MazeSpawnPoint => _mazeSpawnPoint;
        public Camera CachedCamera => _cachedCamera;
        public Transform UISpawnPoint => _uiSpawnPoint;
        public Transform HUDSpawnPoint => _hudSpawnPoint;
        public LifetimeScope LifetimeScope => _lifetimeScope;
    }
}