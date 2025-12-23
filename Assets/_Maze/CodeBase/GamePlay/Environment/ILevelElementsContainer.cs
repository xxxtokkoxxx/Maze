using _Maze.CodeBase.GamePlay.Player;

namespace _Maze.CodeBase.GamePlay.Environment
{
    public interface ILevelElementsContainer
    {
        PlayerView GetPlayer();
    }
}