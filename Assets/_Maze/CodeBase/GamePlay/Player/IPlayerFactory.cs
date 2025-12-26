namespace _Maze.CodeBase.GamePlay.Player
{
    public interface IPlayerFactory
    {
        void SetPlayerReference();
        IPlayer GetPlayer();
        void DestroyPlayerView();
    }
}