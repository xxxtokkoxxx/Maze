using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

namespace _Maze.CodeBase.Infrastructure.ResourcesManagement
{
    public class AssetsLoaderService : IAssetsLoaderService
    {
        private List<LoadedAsset> _cache = new();

        public async Task<TAssetType> LoadAsset<TAssetType>(string path) where TAssetType : Object
        {
            bool assetExist = TryToGetAssetFromCache(path, out TAssetType asset);

            if (assetExist)
            {
                return asset;
            }

            AsyncOperationHandle<TAssetType> operationHandle;

            try
            {
                operationHandle = Addressables.LoadAssetAsync<TAssetType>(path);
                await operationHandle.Task;
            }
            catch (Exception e)
            {
                throw new InvalidDataException($"Failed to load asset: {path}", e);
            }

            TAssetType result = operationHandle.Result;
            _cache.Add(new LoadedAsset(path, operationHandle, result));
            return result;
        }

        public async Task<IList<TAssetType>> LoadAssets<TAssetType>(string label)
        {
            bool assetExist = TryToGetAssetFromCache(label, out IList<TAssetType> asset);
            AsyncOperationHandle<IList<GameObject>> loadWithIResourceLocations;

            if (assetExist)
            {
                return asset;
            }

            try
            {
                IList<IResourceLocation> locations;
                locations = await GetResourceLocations(label);

                loadWithIResourceLocations = Addressables.LoadAssetsAsync<GameObject>(locations, obj => { });
                await loadWithIResourceLocations.Task;
            }
            catch (Exception e)
            {
                throw new InvalidDataException($"Failed to load asset: {label}", e);
            }

            IList<TAssetType> handle = new List<TAssetType>();
            foreach (GameObject res in loadWithIResourceLocations.Result)
            {
                handle.Add(res.GetComponent<TAssetType>());
            }

            LoadedAsset cache = new LoadedAsset(label, loadWithIResourceLocations, handle);
            _cache.Add(cache);

            return handle;
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

        public void Release(string address)
        {
            LoadedAsset objectToRelease = _cache.FirstOrDefault(a => a.Address == address);

            if (objectToRelease == null)
            {
                return;
            }

            ReleaseInternal(objectToRelease.Handle);
            _cache.Remove(objectToRelease);
        }

        private void ReleaseInternal(AsyncOperationHandle handle)
        {
            try
            {
                Addressables.Release(handle);
            }
            catch (Exception e)
            {
                throw new InvalidDataException($"Failed to release asset ", e);
            }
        }

        private async Task<IList<IResourceLocation>> GetResourceLocations(string label)
        {
            AsyncOperationHandle<IList<IResourceLocation>> handle;

            try
            {
                handle = Addressables.LoadResourceLocationsAsync(label, typeof(Object));
                await handle.Task;
            }
            catch (Exception e)
            {
                throw new InvalidDataException($"Failed to load asset: {label}", e);
            }

            IList<IResourceLocation> loadedLocations = handle.Result;
            List<string> loadedKeys = new List<string>();
            IList<IResourceLocation> loadedLocationsReturn = new List<IResourceLocation>();

            foreach (IResourceLocation location in loadedLocations)
            {
                if (loadedKeys.Contains(location.PrimaryKey))
                    continue;

                loadedKeys.Add(location.PrimaryKey);
                loadedLocationsReturn.Add(location);
            }

            _cache.Add(new LoadedAsset(AssetsDataPath.ResourceLocation + label, handle, loadedLocationsReturn));
            return loadedLocationsReturn;
        }
    }
}