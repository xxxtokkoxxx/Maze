using UnityEngine;

namespace _Maze.CodeBase.Infrastructure
{
    public class MonoBehavioursProvider : MonoBehaviour, IMonoBehavioursProvider
    {
        [SerializeField] private Transform _mazeSpawnPoint;
        public Transform MazeSpawnPoint => _mazeSpawnPoint;
    }
}