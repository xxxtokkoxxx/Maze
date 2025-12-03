using System;

namespace _Maze.CodeBase.UI.GameOver
{
    public class GameOverUICallbacks
    {
        public event Action OnGoToMainMenu;
        public event Action OnRestartGame;

        public void GoToMainMenu()
        {
            OnGoToMainMenu?.Invoke();
        }

        public void RestartGame()
        {
            OnRestartGame?.Invoke();
        }
    }
}