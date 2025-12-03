namespace _Maze.CodeBase.UI.GameOver
{
    public class GameOverView : BaseView
    {
        private GameOverUICallbacks _callbacks;
        public override ViewType ViewType => ViewType.GameOver;

        public void Initialize(GameOverUICallbacks callbacks)
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
    }
}