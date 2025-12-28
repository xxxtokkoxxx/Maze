#if UNITY_EDITOR
using _Maze.CodeBase.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Maze.CodeBase.Utilities
{
    public class InitSceneLoader : MonoBehaviour
    {
        public static InitSceneLoader Instance => _instance;

        [SerializeField] private bool _isGameLevel = true;
        private static InitSceneLoader _instance;

        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }

            _instance = this;

            if (SceneManager.GetActiveScene().name != SceneNames.Bootstrap)
            {
                if (_isGameLevel)
                {
                    GameRunnerFromAnyScene.GameSceneName = SceneManager.GetActiveScene().name;
                }

                SceneManager.LoadScene(SceneNames.Bootstrap);
            }
        }
    }
}
#endif