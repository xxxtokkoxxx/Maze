using _Maze.CodeBase.GamePlay.Maze;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Maze.CodeBase.Infrastructure
{
    public class DiBinder : LifetimeScope
    {
        [SerializeField] private MonoBehavioursProvider _monoBehavioursProvider;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IMazeRenderer, MazeRenderer>(Lifetime.Singleton);
            builder.Register<IMazeGenerator, MazeGenerator>(Lifetime.Singleton);
            builder.Register<IMazeFactory, MazeFactory>(Lifetime.Singleton);
            builder.Register<IAssetsLoaderService, AssetsLoaderService>(Lifetime.Singleton);

            builder.RegisterComponent(_monoBehavioursProvider).AsImplementedInterfaces();
        }
    }
}