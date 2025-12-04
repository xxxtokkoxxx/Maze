using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.Infrastructure.ResourcesManagement
{
    public interface IAssetsLoaderService
    {
        Task<TAssetType> LoadAsset<TAssetType>(string path) where TAssetType : Object;
        Task<IList<TAssetType>> LoadAssets<TAssetType>(string label);
        void Release(string address);
    }
}