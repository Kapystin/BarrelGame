using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BarrelGame.Scripts.UI.InGamePanel
{
    public class InGamePanelView : BaseView
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private Image _filledImage;
        [SerializeField] private Button _controllerSettingsButton;
        
        [Space]
        [Header("Controller Settings Panel")]
        [SerializeField] private GameObject _controllerSettingsPanel;
        [SerializeField] private Button _acceptSettingsButton;
        [SerializeField] private Toggle _keyboardInputToggle;
        [SerializeField] private Toggle _touchInputToggle;
        
        public Action<bool> OnKeyboardInputClick;
        public Action<bool> OnTouchInputClick;

        public Action OnControllerSettingsClick;
        public Action OnAcceptSettingsClick;

        public override void AddListeners()
        {
            _acceptSettingsButton.onClick.AddListener(()=> OnAcceptSettingsClick?.Invoke());
            _controllerSettingsButton.onClick.AddListener(()=> OnControllerSettingsClick?.Invoke());
            
            _keyboardInputToggle.onValueChanged.AddListener((value)=>OnKeyboardInputClick?.Invoke(value));
            _touchInputToggle.onValueChanged.AddListener((value)=>OnTouchInputClick?.Invoke(value));
        }

        public override void RemoveListeners()
        {
            _acceptSettingsButton.onClick.RemoveAllListeners();
            _controllerSettingsButton.onClick.RemoveAllListeners();
            
            _keyboardInputToggle.onValueChanged.RemoveAllListeners();
            _touchInputToggle.onValueChanged.RemoveAllListeners();
        }

        public void SetControllerSettingsPanelState(bool value)
        {
            _controllerSettingsPanel.SetActive(value);
        }

        public void SetTimerValue(float startValue, float currentValue)
        {
            _timerText.text = $"{Mathf.RoundToInt(currentValue)}";
            _filledImage.fillAmount = currentValue / startValue;
        }
        
        public void SetScoreValue(int value)
        {
            _scoreText.text = $"{value}";
        }
    }
}
