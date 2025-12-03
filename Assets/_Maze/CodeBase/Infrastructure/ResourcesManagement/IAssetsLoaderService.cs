using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.Infrastructure.ResourcesManagement
{
    public interface IAssetsLoaderService
    {
        Task<GameObject> LoadAsset(string path);
        void Release(string address);
    }
}