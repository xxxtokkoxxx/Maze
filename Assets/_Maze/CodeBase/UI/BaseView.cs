using System;
using UnityEngine;

namespace _Maze.CodeBase.UI
{
    public abstract class BaseView : MonoBehaviour, IView
    {
        public Guid Id { get; set; }
        public abstract ViewType ViewType { get; }
        public abstract void Show();
        public abstract void Hide();
        public GameObject GameObject => gameObject;
    }
}