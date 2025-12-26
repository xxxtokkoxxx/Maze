using _Maze.CodeBase.GamePlay.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Maze.CodeBase.GamePlay.Environment
{
    public class LevelElementsContainer : MonoBehaviour, ILevelElementsContainer
    {
        [SerializeField] private Transform _collectiblesTileMap;
        [SerializeField] private Transform _playerTilemap;
        private IObjectResolver _objectResolver;

        [Inject]
        public void Inject(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        private void Start()
        {
            InjectsObjects();
        }

        public PlayerView GetPlayer()
        {
            return _playerTilemap.GetComponentInChildren<PlayerView>();
        }

        private void InjectsObjects()
        {
            for (int i = 0; i < _playerTilemap.childCount; i++)
            {
                _objectResolver.InjectGameObject(_playerTilemap.GetChild(i).gameObject);
            }

            for (int i = 0; i < _collectiblesTileMap.childCount; i++)
            {
                _objectResolver.InjectGameObject(_collectiblesTileMap.GetChild(i).gameObject);
            }
        }
    }
}