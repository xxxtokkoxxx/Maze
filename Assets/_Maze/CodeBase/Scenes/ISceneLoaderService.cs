using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Scenes
{
    public interface ISceneLoaderService
    {
        UniTask LoadScene(string sceneName, bool releasePreviousScene = false);
    }
}