namespace _Maze.CodeBase.GamePlay.Environment
{
    public interface ILevelElement
    {
        void SaveState();
        void RestoreState();
    }
}