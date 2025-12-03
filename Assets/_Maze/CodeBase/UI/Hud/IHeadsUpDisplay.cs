namespace _Maze.CodeBase.UI.Hud
{
    public interface IHeadsUpDisplay
    {
        void UpdateStepsCount(int stepsCount);
        void UpdateTimer(int minutes, int seconds);
    }
}