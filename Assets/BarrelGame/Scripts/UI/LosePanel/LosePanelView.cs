using System;
using UnityEngine;
using UnityEngine.UI;

namespace BarrelGame.Scripts.UI.LosePanel
{
    public class LosePanelView : BaseView
    {
        [SerializeField] private Button _restartButton;

        public Action OnRestartButtonClick;
        
        public override void AddListeners()
        {
            _restartButton.onClick.AddListener((() => OnRestartButtonClick?.Invoke()));    
        }

        public override void RemoveListeners()
        {
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}