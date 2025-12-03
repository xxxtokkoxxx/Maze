using TMPro;
using UnityEngine;

namespace _Maze.CodeBase.UI.MainMenu
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private GameObject _loadButton;

        [SerializeField] private TextMeshProUGUI _mazeHeightText;
        [SerializeField] private TextMeshProUGUI _mazeWidthText;
        [SerializeField] private TextMeshProUGUI _exitsCount;

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

        public void SetMazeSizeHorizontal(int mazeWidth)
        {
            _callbacks.SetMazeWidth(mazeWidth);
        }

        public void SetMazeSizeVertical(int mazeHeight)
        {
            _callbacks.SetMazeHeight(mazeHeight);
        }

        public void SetExitsCount(int exitsCount)
        {
            _callbacks.SetExistsCount(exitsCount);
        }

        public void SetLoadButtonEnabled(bool isEnabled)
        {
            _loadButton.gameObject.SetActive(isEnabled);
        }

        public void UpdateExistsCountText(int existsCount)
        {
            _exitsCount.text = $"{existsCount}";
        }

        public void UpdateMazeWidthText(int mazeWidth)
        {
            _mazeWidthText.text = $"{mazeWidth}";
        }

        public void UpdateMazeHeightText(int mazeHeight)
        {
            _mazeHeightText.text = $"{mazeHeight}";
        }
    }
}