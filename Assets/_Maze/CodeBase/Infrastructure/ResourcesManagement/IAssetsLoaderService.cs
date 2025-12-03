using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.Infrastructure.ResourcesManagement
{
    public interface IAssetsLoaderService
    {
        Task<GameObject> LoadAsset(string path);
        Task<IList<TAssetType>> LoadAssets<TAssetType>(string label);
        void Release(string address);
    }
}