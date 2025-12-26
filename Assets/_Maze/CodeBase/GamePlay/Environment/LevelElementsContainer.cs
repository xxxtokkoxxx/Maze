using _Maze.CodeBase.GamePlay.Player;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Environment
{
    public class LevelElementsContainer : MonoBehaviour, ILevelElementsContainer
    {
        [SerializeField] private Transform _collectiblesTileMap;
        [SerializeField] private Transform _playerTilemap;

        public PlayerView GetPlayer()
        {
            return _playerTilemap.GetComponentInChildren<PlayerView>();
        }
    }
}