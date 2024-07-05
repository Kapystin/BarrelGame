using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BarrelGame.Scripts.UI.WinPanel
{
    public class WinPanelView: BaseView
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _scoreText;
         

        public Action OnRestartButtonClick;
        
        public override void AddListeners()
        {
            _restartButton.onClick.AddListener((() => OnRestartButtonClick?.Invoke()));    
        }

        public override void RemoveListeners()
        {
            _restartButton.onClick.RemoveAllListeners();
        }

        public void SetScoreValue(int value)
        {
            _scoreText.text = $"{value}";
        }
    }
}