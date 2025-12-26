using System;
using System.Threading.Tasks;
using _Maze.CodeBase.UI;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using Random = System.Random;

namespace _Maze.CodeBase.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private IUIService _uiService;
        private IUIViewsFactory _uiViewsFactory;

        [Inject]
        public void Inject(IUIService uiService, IUIViewsFactory uiViewsFactory)
        {
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private async void Start()
        {
            await Task.WhenAll(_uiViewsFactory.LoadViews());

            _uiService.ShowWindow(ViewType.MainMenu);
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                int result = 0;
                int result2 = 0;
                int hashCode = Guid.NewGuid().GetHashCode();

                Random random = new Random(math.abs(hashCode));

                for (int i = 0; i < 1000000; i++)
                {
                    int rand = random.Next();
                    if (rand % 100 <= 20)
                    {
                        result++;
                    }
                    var rand2 = UnityEngine.Random.Range(0, Mathf.Abs(hashCode));

                    if (rand2 % 100 <= 20)
                    {
                        result2++;
                    }
                }

                Debug.Log(result);
                Debug.Log(result2);

            }
        }
    }
}