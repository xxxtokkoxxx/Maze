namespace _Maze.CodeBase.GamePlay.GameSession
{
    public interface IGamePlayProcessor
    {
        void Run();
        void Reset();
        void Stop();
    }
}