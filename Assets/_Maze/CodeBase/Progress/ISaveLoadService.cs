using _Maze.CodeBase.Data;

namespace _Maze.CodeBase.Progress
{
    public interface ISaveLoadService
    {
        void SaveGame(GameProgressData gameProgressData);
        GameProgressData LoadGame();
        bool SaveExists();
    }
}