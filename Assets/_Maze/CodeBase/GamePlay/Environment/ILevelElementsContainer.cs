using _Maze.CodeBase.GamePlay.Player;

namespace _Maze.CodeBase.GamePlay.Environment
{
    public interface ILevelElementsContainer
    {
        void InjectDependenciesIntoLevelObjects();
        void RestoreLevelElementsInitialState();
        void SaveElementsState();
        PlayerView GetPlayer();
    }
}