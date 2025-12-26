namespace _Maze.CodeBase.Progress
{
    public interface ISaveLoadService
    {
        void SaveGame();
        void LoadGame();
        bool SaveExists();
    }
}