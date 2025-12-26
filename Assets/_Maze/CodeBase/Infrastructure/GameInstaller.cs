using System;
using _Maze.CodeBase.Configuration;
using _Maze.CodeBase.GamePlay.Camera;
using _Maze.CodeBase.GamePlay.Environment;
using _Maze.CodeBase.GamePlay.GameSession;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Infrastructure.EventBus;
using _Maze.CodeBase.Input;
using _Maze.CodeBase.UI;
using _Maze.CodeBase.UI.GameOver;
using _Maze.CodeBase.UI.Hud;
using _Maze.CodeBase.UI.Pause;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Maze.CodeBase.Infrastructure
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private LevelElementsContainer _levelEnvironmentContainer;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_levelEnvironmentContainer).AsImplementedInterfaces();

            builder.Register<IPlayerFactory, PlayerFactory>(Lifetime.Singleton);
            builder.Register<IPlayerMovementSystem, IInitializable, IDisposable, PlayerMovementSystem>(Lifetime.Singleton);
            builder.Register<IGameSessionRunner, IInitializable, GameSessionRunner>(Lifetime.Singleton);
            builder.Register<IInputStateProvider, InputStateProvider>(Lifetime.Singleton);
            builder.Register<ICameraFollowSystem, ITickable, CameraFollowSystem>(Lifetime.Singleton);
            builder.Register<IGamePlayProcessor, GamePlayProcessor>(Lifetime.Singleton);
            builder.Register<IGamePauseProcessor, GamePauseProcessor>(Lifetime.Singleton);
            builder.Register<IGameConfiguration, GameConfiguration>(Lifetime.Singleton);
            builder.Register<IGameplayEventBus, GameplayEventBus>(Lifetime.Singleton);
            builder.Register<IViewController, IInitializable, IDisposable, GameOverUiController>(Lifetime.Singleton);
            builder.Register<IViewController, IDisposable, IInitializable, PauseUIController>(Lifetime.Singleton);
            builder.Register<IViewController, IHeadsUpDisplay, IInitializable, IDisposable, HeadsUpDisplayUiController>(Lifetime.Singleton);
        }
    }
}