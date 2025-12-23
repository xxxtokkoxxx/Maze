using UnityEngine;

namespace _Maze.CodeBase.UI.MainMenu
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private GameObject _loadButton;

        private MainMenuCallbacks _callbacks;

        public override ViewType ViewType => ViewType.MainMenu;

        public void Initialize(MainMenuCallbacks callbacks)
        {
            _callbacks = callbacks;
        }

        public void StartGame()
        {
            _callbacks.StartGame();
        }

        public void LoadGame()
        {
            _callbacks.LoadGame();
        }

        public void SetLoadButtonEnabled(bool isEnabled)
        {
            _loadButton.gameObject.SetActive(isEnabled);
        }
    }
}