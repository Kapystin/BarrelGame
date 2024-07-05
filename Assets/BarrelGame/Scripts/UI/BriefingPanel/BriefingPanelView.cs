using System;
using UnityEngine;
using UnityEngine.UI;

namespace BarrelGame.Scripts.UI.BriefingPanel
{
    public class BriefingPanelView : BaseView
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Toggle _keyboardInputToggle;
        [SerializeField] private Toggle _touchInputToggle;

        public Action OnStartGameClick;
        public Action<bool> OnKeyboardInputClick;
        public Action<bool> OnTouchInputClick;
        
        public override void AddListeners()
        {
            _startGameButton.onClick.AddListener(() => OnStartGameClick?.Invoke());
            _keyboardInputToggle.onValueChanged.AddListener((value)=>OnKeyboardInputClick?.Invoke(value));
            _touchInputToggle.onValueChanged.AddListener((value)=>OnTouchInputClick?.Invoke(value));
        }

        public override void RemoveListeners()
        {
            _startGameButton.onClick.RemoveAllListeners();
        }
    }
}
