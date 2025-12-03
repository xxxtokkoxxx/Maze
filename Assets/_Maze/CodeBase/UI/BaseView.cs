using System;
using UnityEngine;

namespace _Maze.CodeBase.UI
{
    public abstract class BaseView : MonoBehaviour, IView
    {
        public Guid Id { get; set; }
        public abstract ViewType ViewType { get; }
        public GameObject GameObject => gameObject;
    }
}