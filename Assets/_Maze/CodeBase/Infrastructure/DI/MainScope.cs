using System;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using _Maze.CodeBase.Infrastructure.StateMachine;
using _Maze.CodeBase.Progress;
using _Maze.CodeBase.Scenes;
using _Maze.CodeBase.UI;
using _Maze.CodeBase.UI.MainMenu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Maze.CodeBase.Infrastructure.DI
{
    public class MainScope : BaseScope
    {
        [SerializeField] private MonoBehavioursProvider _monoBehavioursProvider;
        public override Scope Scope => Scope.Main;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("Call register component");

            builder.Register<IAssetsLoaderService, AssetsLoaderService>(Lifetime.Singleton);
            builder.Register<IUIService, UIService>(Lifetime.Singleton);
            builder.Register<IUIViewsFactory, UIViewsFactory>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
            builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Singleton);
            builder.Register<IViewController, IDisposable, IInitializable, MainMenuUIController>(Lifetime.Singleton);
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<IState, IInitializable, IDisposable, BootstrapState>(Lifetime.Singleton);
            builder.Register<IState, IInitializable, IDisposable, GameMenuState>(Lifetime.Singleton);
            builder.Register<IState, IInitializable, IDisposable, GameLoadingState>(Lifetime.Singleton);

            builder.RegisterComponent(_monoBehavioursProvider).AsImplementedInterfaces();
        }
    }
}