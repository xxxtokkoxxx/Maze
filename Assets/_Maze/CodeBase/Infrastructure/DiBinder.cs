using System;
using _Maze.CodeBase.Configuration;
using _Maze.CodeBase.GamePlay.Camera;
using _Maze.CodeBase.GamePlay.Environment;
using _Maze.CodeBase.GamePlay.GameSession;
using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.GamePlay.Pause;
using _Maze.CodeBase.GamePlay.Player;
using _Maze.CodeBase.Infrastructure.EventBus;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using _Maze.CodeBase.Input;
using _Maze.CodeBase.Progress;
using _Maze.CodeBase.UI;
using _Maze.CodeBase.UI.GameOver;
using _Maze.CodeBase.UI.Hud;
using _Maze.CodeBase.UI.MainMenu;
using _Maze.CodeBase.UI.Pause;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Maze.CodeBase.Infrastructure
{
    public class DiBinder : LifetimeScope
    {
        [SerializeField] private MonoBehavioursProvider _monoBehavioursProvider;
        [SerializeField] private LevelElementsContainer _levelEnvironmentContainer;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IPlayerFactory, PlayerFactory>(Lifetime.Singleton);
            builder.Register<IPlayerMovementSystem, IInitializable, IDisposable, PlayerMovementSystem>(Lifetime.Singleton);
            builder.Register<IGameSessionRunner, GameSessionRunner>(Lifetime.Singleton);
            builder.Register<IInputStateProvider, InputStateProvider>(Lifetime.Singleton);
            builder.Register<ICameraFollowSystem, ITickable, CameraFollowSystem>(Lifetime.Singleton);
            builder.Register<IGamePlayProcessor, GamePlayProcessor>(Lifetime.Singleton);
            builder.Register<IAssetsLoaderService, AssetsLoaderService>(Lifetime.Singleton);
            builder.Register<IUIService, UIService>(Lifetime.Singleton);
            builder.Register<IUIViewsFactory, UIViewsFactory>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
            builder.Register<IGamePauseProcessor, GamePauseProcessor>(Lifetime.Singleton);
            builder.Register<IViewController, MainMenuUIController>(Lifetime.Singleton);
            builder.Register<IViewController, GameOverUiController>(Lifetime.Singleton);
            builder.Register<IViewController, PauseUIController>(Lifetime.Singleton);
            builder.Register<IViewController, IHeadsUpDisplay, HeadsUpDisplayUiController>(Lifetime.Singleton);
            builder.Register<IGameRuntimeDataContainer, GameRuntimeDataContainerContainer>(Lifetime.Singleton);
            builder.Register<IGameConfiguration, GameConfiguration>(Lifetime.Singleton);
            builder.Register<IGameplayEventBus, GameplayEventBus>(Lifetime.Singleton);
            builder.Register<ILevelElementsContainer, LevelElementsContainer>(Lifetime.Singleton);

            builder.RegisterComponent(_monoBehavioursProvider).AsImplementedInterfaces();
        }
    }
}