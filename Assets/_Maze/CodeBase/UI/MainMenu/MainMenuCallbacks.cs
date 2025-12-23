using System;

namespace _Maze.CodeBase.UI.MainMenu
{
    public class MainMenuCallbacks
    {
        public event Action OnStartGame;
        public event Action OnLoadGame;

        public void StartGame()
        {
            OnStartGame?.Invoke();
        }

        public void LoadGame()
        {
            OnLoadGame?.Invoke();
        }
    }
}