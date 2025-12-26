using System.Collections.Generic;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using Cysharp.Threading.Tasks;

namespace _Maze.CodeBase.Infrastructure.DI
{
    public class DiContainersFactory
    {
        private readonly IAssetsLoaderService _assetsLoaderService;
        private IList<BaseScope> _scopesReferences;

        public DiContainersFactory(IAssetsLoaderService assetsLoaderService)
        {
            _assetsLoaderService = assetsLoaderService;
        }

        public async UniTask LoadChildrenScopes()
        {
            _scopesReferences = await _assetsLoaderService.LoadAssets<BaseScope>(AssetsDataPath.Scopes);
        }
    }
}