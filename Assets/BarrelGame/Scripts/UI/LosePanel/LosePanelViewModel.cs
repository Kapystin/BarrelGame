using BarrelGame.Scripts.Interface.UI;
using UnityEngine.SceneManagement;

namespace BarrelGame.Scripts.UI.LosePanel
{
    public class LosePanelViewModel : IViewModel
    {
        private LosePanelView _view;
        private LosePanelModel _model;
        
        public LosePanelViewModel(LosePanelView view, LosePanelModel model)
        {
            _view = view;
            _model = model;
        }
        
        public void AddListeners()
        {
            _view.OnRestartButtonClick += OnRestartButtonClick;
        }

        public void RemoveListeners()
        {
            _view.OnRestartButtonClick -= OnRestartButtonClick;
        }

        private void OnRestartButtonClick()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}