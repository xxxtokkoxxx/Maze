using System.Collections.Generic;
using _Maze.CodeBase.Input;
using _Maze.CodeBase.UI;
using UnityEngine;

namespace _Maze.CodeBase.GamePlay.Pause
{
    public class GamePauseProcessor : IGamePauseProcessor
    {
        private bool _isPaused;
        private List<IPauseable> _pauseables = new();

        private readonly IInputStateProvider _inputStateProvider;
        private readonly IUIService _uiService;

        public GamePauseProcessor(IInputStateProvider inputStateProvider, IUIService uiService)
        {
            _inputStateProvider = inputStateProvider;
            _uiService = uiService;
        }

        public void Initialize()
        {
            _isPaused = false;
            _pauseables.Clear();
            _inputStateProvider.OnPaused += Pause;
        }

        public void Dispose()
        {
            _isPaused = false;
            _inputStateProvider.OnPaused -= Pause;
        }

        public void SetPaused(bool isPaused)
        {
            _isPaused = isPaused;
            Notify(isPaused);
        }

        private void Pause()
        {
            PauseInternal(!_isPaused);
        }

        public void AddPausable(IPauseable pauseable)
        {
            if (!_pauseables.Contains(pauseable))
            {
                _pauseables.Add(pauseable);
            }
        }

        public void RemovePausable(IPauseable pauseable)
        {
            _pauseables.Remove(pauseable);
        }

        private void PauseInternal(bool isPaused)
        {
            _isPaused = isPaused;
            Notify(_isPaused);
            ShowPauseMenu(_isPaused);
        }

        private void Notify(bool isPaused)
        {
            foreach (IPauseable pauseable in _pauseables)
            {
                if (pauseable != null)
                {
                    pauseable.SetPaused(isPaused);
                }
            }
        }

        private void ShowPauseMenu(bool isPaused)
        {
            Debug.Log(isPaused);
            if (isPaused)
            {
                _uiService.ShowWindow(ViewType.Pause);
            }
            else
            {
                _uiService.HideWindow(ViewType.Pause);
            }
        }
    }
}