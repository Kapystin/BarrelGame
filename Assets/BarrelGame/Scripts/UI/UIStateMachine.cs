using System;
using BarrelGame.Scripts.Interface.UI;
using BarrelGame.Scripts.UI.UIStates;
using UnityEngine;

namespace BarrelGame.Scripts.UI
{
    public class UIStateMachine : MonoBehaviour
    {
        public static UIStateMachine Instance => _instance;
        private static UIStateMachine _instance;
            
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private Canvas _dynamicCanvas;
        
        private UIBaseState _currentUIState;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != null)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _currentUIState = new BriefingUIState();
            _currentUIState.Init(_mainCanvas.transform);
        }

        public void SetState(UIBaseState state)
        {
            object data = null;
            var parentTransform = _mainCanvas.transform;
            
            if (state is InGameUIState)
            {
                parentTransform = _dynamicCanvas.transform;
            }

            if (state is WinUIState)
            {
                data = PlayerPrefs.GetInt(PlayerPrefsNaming.SCORE);
            }
            
            _currentUIState = state;
            _currentUIState.Init(parentTransform, data);
        }
    }
}