using System;
using System.Threading.Tasks;
using UnityEngine;

namespace _Maze.CodeBase.UI
{
    public interface IUIViewsFactory
    {
        Task LoadViews();
        TView CreateView<TView>(ViewType viewType, Transform parent = null) where TView : BaseView;
        void DestroyView(Guid id);
    }
}