using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Maze.CodeBase.Infrastructure.ResourcesManagement
{
    public class LoadedAsset
    {
        public string Address;
        public AsyncOperationHandle Handle;
        public object LoadedObject;

        public LoadedAsset(string address, AsyncOperationHandle handle, object loadedObject)
        {
            Handle = handle;
            Address = address;
            LoadedObject = loadedObject;
        }
    }
}