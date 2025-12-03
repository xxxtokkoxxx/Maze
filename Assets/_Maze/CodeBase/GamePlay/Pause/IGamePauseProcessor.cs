namespace _Maze.CodeBase.GamePlay.Pause
{
    public interface IGamePauseProcessor
    {
        void Initialize();
        void Dispose();
        void SetPaused(bool isPaused);
        void AddPausable(IPauseable pauseable);
        void RemovePausable(IPauseable pauseable);
    }
}