using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace _Maze.CodeBase.Infrastructure.ResourcesManagement
{
    public interface IAssetsLoaderService
    {
        UniTask<TAssetType> LoadAsset<TAssetType>(string path) where TAssetType : Object;
        UniTask<IList<TAssetType>> LoadAssets<TAssetType>(string label);
        UniTask<SceneInstance> LoadScene(string path);
        void Release(string address);
    }
}