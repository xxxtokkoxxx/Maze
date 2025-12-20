using System;
using System.Collections.Generic;

namespace _Maze.CodeBase.Infrastructure.EventBus
{
    public class GameplayEventBus : IGameplayEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _eventHandlers = new();

        public void Subscribe<TEvent>(Action<TEvent> eventHandler) where TEvent : IGameplayEvent
        {
            Type eventType = typeof(TEvent);

            if (!_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType] = new List<Delegate>();
            }

            _eventHandlers[eventType].Add(eventHandler);
        }

        public void UnSubscribe<TEvent>(Action<TEvent> eventHandler) where TEvent : IGameplayEvent
        {
            Type eventType = typeof(TEvent);

            bool containsValue = _eventHandlers.TryGetValue(eventType, out List<Delegate> handlers);

            if (containsValue)
            {
                _eventHandlers[eventType].Remove(eventHandler);
            }

            _eventHandlers[eventType].Add(eventHandler);
        }

        public void Publish<TEvent>(TEvent eventData) where TEvent : IGameplayEvent
        {
            bool containsValue = _eventHandlers.TryGetValue(eventData.GetType(), out List<Delegate> handlers);

            if (containsValue)
            {
                Delegate[] temp = handlers.ToArray();

                foreach (Delegate handler in temp)
                {
                    if (handler is Action<TEvent> eventHandler)
                    {
                        eventHandler(eventData);
                    }
                }
            }
        }
    }
}