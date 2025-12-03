namespace _Maze.CodeBase.UI.Pause
{
    public class PauseView : BaseView
    {
        private PauseUICallbacks _callbacks;
        public override ViewType ViewType => ViewType.Pause;

        public void Initialize(PauseUICallbacks callbacks)
        {
            _callbacks = callbacks;
        }

        public void GoToMainMenu()
        {
            _callbacks.GoToMainMenu();
        }

        public void RestartGame()
        {
            _callbacks.RestartGame();
        }

        public void OnSaveGame()
        {
            _callbacks.SaveGame();
        }

        public void ResumeGame()
        {
            _callbacks.ResumeGame();
        }
    }
}