using System;

namespace _Maze.CodeBase.UI.MainMenu
{
    public class MainMenuCallbacks
    {
        public event Action OnStartGame;
        public event Action<int> OnSetMazeWidth;
        public event Action<int> OnSetMazeHeight;
        public event Action<int> OnSetExistsCount;
        public event Action OnLoadGame;

        public void StartGame()
        {
            OnStartGame?.Invoke();
        }

        public void SetMazeWidth(int horizontalSize)
        {
            OnSetMazeWidth?.Invoke(horizontalSize);
        }

        public void SetMazeHeight(int verticalSize)
        {
            OnSetMazeHeight?.Invoke(verticalSize);
        }

        public void SetExistsCount(int existsCount)
        {
            OnSetExistsCount?.Invoke(existsCount);
        }

        public void LoadGame()
        {
            OnLoadGame?.Invoke();
        }
    }
}