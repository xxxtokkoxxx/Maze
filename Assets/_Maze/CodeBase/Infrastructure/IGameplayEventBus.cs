using System;

namespace _Maze.CodeBase.Infrastructure
{
    public interface IGameplayEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> eventHandler) where TEvent : IGameplayEvent;
        void UnSubscribe<TEvent>(Action<TEvent> eventHandler) where TEvent : IGameplayEvent;
        void Publish<TEvent>(TEvent eventData) where TEvent : IGameplayEvent;
    }
}