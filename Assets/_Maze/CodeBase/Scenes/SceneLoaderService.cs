using _Maze.CodeBase.Infrastructure;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Maze.CodeBase.Scenes
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;
        private readonly IAssetsLoaderService _assetsLoaderService;

        public SceneLoaderService(IMonoBehavioursProvider monoBehavioursProvider, IAssetsLoaderService assetsLoaderService)
        {
            _monoBehavioursProvider = monoBehavioursProvider;
            _assetsLoaderService = assetsLoaderService;
        }

        public async UniTask LoadScene(string sceneName, bool releasePreviousScene = false)
        {
            using (LifetimeScope.EnqueueParent(_monoBehavioursProvider.LifetimeScope))
            {
                await _assetsLoaderService.LoadScene(sceneName);
            }
        }
    }
}