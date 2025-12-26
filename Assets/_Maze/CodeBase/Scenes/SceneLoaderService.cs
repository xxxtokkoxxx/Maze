using _Maze.CodeBase.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace _Maze.CodeBase.Scenes
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly IMonoBehavioursProvider _monoBehavioursProvider;

        public SceneLoaderService(IMonoBehavioursProvider monoBehavioursProvider)
        {
            _monoBehavioursProvider = monoBehavioursProvider;
        }

        public async UniTask LoadScene(string sceneName)
        {
            using (LifetimeScope.EnqueueParent(_monoBehavioursProvider.LifetimeScope))
            {
                await SceneManager.LoadSceneAsync(sceneName);
            }
        }
    }
}