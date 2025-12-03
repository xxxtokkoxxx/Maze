using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Maze.CodeBase.Infrastructure.ResourcesManagement
{
    public class AssetsLoaderService : IAssetsLoaderService
    {
        private List<LoadedAsset> _cache = new();

        public async Task<GameObject> LoadAsset(string path)
        {
            bool assetExist = TryToGetAssetFromCache(path, out GameObject asset);

            if (assetExist)
            {
                return asset;
            }

            AsyncOperationHandle<GameObject> operationHandle;

            try
            {
                operationHandle = Addressables.LoadAssetAsync<GameObject>(path);
                await operationHandle.Task;
            }
            catch (Exception e)
            {
                throw new InvalidDataException($"Failed to load asset: {path}", e);
            }

            _cache.Add(new LoadedAsset(path, operationHandle, operationHandle.Result));
            return operationHandle.Result;
        }

        private bool TryToGetAssetFromCache<TAsset>(string key, out TAsset asset) where TAsset : class
        {
            asset = default;

            LoadedAsset loadedAsset = _cache.FirstOrDefault(a => a.Address == key);
            if (loadedAsset == null)
            {
                return false;
            }

            asset = loadedAsset.LoadedObject as TAsset;
            return true;
        }

        private void ReleaseInternal(AsyncOperationHandle handle)
        {
            try
            {
                Addressables.Release(handle);
            }
            catch (Exception e)
            {
                throw new InvalidDataException($"Failed to load asset", e);
            }
        }
    }
}