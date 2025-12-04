using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Maze.CodeBase.Infrastructure;
using _Maze.CodeBase.Infrastructure.ResourcesManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Maze.CodeBase.UI
{
    public class UIViewsFactory : IUIViewsFactory
    {
        private IList<BaseView> _viewReferences;
        private List<BaseView> _activeViews = new();

        private readonly IAssetsLoaderService _loaderService;
        private readonly IMonoBehavioursProvider _monoBehaviourProvider;

        public UIViewsFactory(IAssetsLoaderService loaderService,
            IMonoBehavioursProvider monoBehaviourProvider)
        {
            _loaderService = loaderService;
            _monoBehaviourProvider = monoBehaviourProvider;
        }

        public async Task LoadViews()
        {
            IList<BaseView> views = await _loaderService.LoadAssets<BaseView>(AssetsDataPath.UIViews);
            _viewReferences = views;
        }

        public TView CreateView<TView>(ViewType viewType, Transform parent = null) where TView : BaseView
        {
            BaseView reference = _viewReferences.FirstOrDefault(a => a.ViewType == viewType);
            if (reference == null)
            {
                Debug.LogError($"View {viewType} not found");
                return null;
            }

            Transform spawnPoint = parent == null ? _monoBehaviourProvider.UISpawnPoint : parent;

            TView view = Object.Instantiate(reference, spawnPoint).GetComponent<TView>();
            view.Id = Guid.NewGuid();

            _activeViews.Add(view);
            return view;
        }

        public void DestroyView(Guid id)
        {
            BaseView view = _activeViews.FirstOrDefault(a => a.Id == id);

            if (view == null)
            {
                Debug.LogError($"View with id: {id} not found");
                return;
            }

            _activeViews.Remove(view);
            Object.Destroy(view.gameObject);
        }
    }
}