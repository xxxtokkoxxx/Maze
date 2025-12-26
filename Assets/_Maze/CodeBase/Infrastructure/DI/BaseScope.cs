using VContainer.Unity;

namespace _Maze.CodeBase.Infrastructure.DI
{
    public abstract class BaseScope : LifetimeScope
    {
        public abstract Scope Scope { get; }
    }
}