using BarrelGame.Scripts.Interface.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BarrelGame.Scripts.UI.WinPanel
{
    public class WinPanelViewModel : IViewModel
    {
        private WinPanelView _view;
        private WinPanelModel _model;
        
        public WinPanelViewModel(WinPanelView view, WinPanelModel model)
        {
            _view = view;
            _model = model;
        }
        
        public void AddListeners()
        {
            _view.OnRestartButtonClick += OnRestartButtonClick;
            
            _model.OnScoreDataReceive += OnScoreDataReceive;
        }

        public void RemoveListeners()
        {
            _view.OnRestartButtonClick -= OnRestartButtonClick;
            
            _model.OnScoreDataReceive -= OnScoreDataReceive;
        }

        private void OnRestartButtonClick()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        private void OnScoreDataReceive(int value)
        {
            _view.SetScoreValue(value);
        }
    }
}