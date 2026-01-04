#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace _Maze.CodeBase.Utilities
{
    public static class UnityEditorAssetLoader
    {
        public static IEnumerable<string> GetResourceLocations<T>(string label)
        {
            AsyncOperationHandle<IList<IResourceLocation>> handle;

            try
            {
                handle = Addressables.LoadResourceLocationsAsync(label, typeof(Object));
                handle.WaitForCompletion();
            }
            catch (Exception e)
            {
                throw new InvalidDataException($"Failed to load asset: {label}", e);
            }

            IEnumerable<IResourceLocation> loadedLocations = handle.Result;
            List<string> loadedKeys = new List<string>();

            foreach (IResourceLocation location in loadedLocations)
            {
                if (loadedKeys.Contains(location.PrimaryKey))
                    continue;

                loadedKeys.Add(location.PrimaryKey);
            }

            return loadedKeys;
        }
    }
}
#endif