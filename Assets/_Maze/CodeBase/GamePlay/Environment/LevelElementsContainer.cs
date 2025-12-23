using _Maze.CodeBase.GamePlay.Player;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Environment
{
    public class LevelElementsContainer : MonoBehaviour, ILevelElementsContainer
    {
        [SerializeField] private Transform _collectibles;
        [SerializeField] private Transform _player;
        
        public PlayerView GetPlayer()
        {
            return _player.GetComponentInChildren<IPlayer>();
        }
    }
}