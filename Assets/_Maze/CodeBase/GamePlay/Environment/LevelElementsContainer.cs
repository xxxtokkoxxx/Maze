using System.Collections.Generic;
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
        private List<ILevelElement> _levelElements = new List<ILevelElement>();

        [Inject]
        public void Inject(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public void InjectDependenciesIntoLevelObjects()
        {
            for (int i = 0; i < _playerTilemap.childCount; i++)
            {
                GameObject element = _playerTilemap.GetChild(i).gameObject;
                bool componentExists = element.TryGetComponent(out ILevelElement levelElement);

                if (!componentExists)
                {
                    Debug.LogError($"Level element:{element.name} is not inherit from ILevelElement");
                    return;
                }

                _objectResolver.InjectGameObject(element);
                _levelElements.Add(levelElement);
            }

            for (int i = 0; i < _collectiblesTileMap.childCount; i++)
            {
                GameObject element = _collectiblesTileMap.GetChild(i).gameObject;
                bool componentExists = element.TryGetComponent(out ILevelElement levelElement);

                if (!componentExists)
                {
                    Debug.LogError($"Level element:{element.name} is not inherit from ILevelElement");
                    return;
                }

                _objectResolver.InjectGameObject(_collectiblesTileMap.GetChild(i).gameObject);
                _levelElements.Add(levelElement);
            }
        }

        public void SaveElementsState()
        {
            foreach (ILevelElement levelElement in _levelElements)
            {
                levelElement.SaveState();
            }
        }

        public PlayerView GetPlayer()
        {
            return _playerTilemap.GetComponentInChildren<PlayerView>();
        }

        public void RestoreLevelElementsInitialState()
        {
            foreach (ILevelElement levelElement in _levelElements)
            {
                levelElement.RestoreState();
            }
        }
    }
}