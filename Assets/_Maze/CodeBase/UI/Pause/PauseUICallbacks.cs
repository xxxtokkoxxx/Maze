using System;

namespace _Maze.CodeBase.UI.Pause
{
    public class PauseUICallbacks
    {
        public event Action OnSave;
        public event Action OnRestartGame;
        public event Action OnResumeGame;
        public event Action OnGoToMainMenu;

        public void SaveGame()
        {
            OnSave?.Invoke();
        }

        public void GoToMainMenu()
        {
            OnGoToMainMenu?.Invoke();
        }

        public void RestartGame()
        {
            OnRestartGame?.Invoke();
        }

        public void ResumeGame()
        {
            OnResumeGame?.Invoke();
        }
    }
}