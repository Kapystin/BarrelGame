using BarrelGame.Scripts.Character;
using BarrelGame.Scripts.Interface.UI;
using UnityEngine;

namespace BarrelGame.Scripts.UI.InGamePanel
{
    public class InGamePanelViewModel : IViewModel
    {
        private InGamePanelView _view;
        private InGamePanelModel _model;

        private bool _controllerSettingsPanelState;
        
        public InGamePanelViewModel(InGamePanelView view, InGamePanelModel model)
        {
            _view = view;
            _model = model;

            _controllerSettingsPanelState = false;
            _view.SetControllerSettingsPanelState(_controllerSettingsPanelState);
        }
        
        public void AddListeners()
        {
            _view.OnControllerSettingsClick += OnControllerSettingsClick;
            _view.OnAcceptSettingsClick += OnAcceptSettingsClick;
            
            _view.OnKeyboardInputClick += OnKeyboardInputClick;
            _view.OnTouchInputClick += OnTouchInputClick;

            _model.OnTimerValueChangeAction += OnTimerValueChange;
            _model.OnScoreValueChangeAction += OnScoreValueChange;
        }

        public void RemoveListeners()
        {
            _view.OnControllerSettingsClick -= OnControllerSettingsClick;
            _view.OnAcceptSettingsClick -= OnAcceptSettingsClick;
            
            _view.OnKeyboardInputClick -= OnKeyboardInputClick;
            _view.OnTouchInputClick -= OnTouchInputClick;
            
            _model.OnTimerValueChangeAction -= OnTimerValueChange;
            _model.OnScoreValueChangeAction -= OnScoreValueChange;
        }

        private void OnControllerSettingsClick()
        {
            _controllerSettingsPanelState = !_controllerSettingsPanelState;
            _view.SetControllerSettingsPanelState(_controllerSettingsPanelState);
        }

        private void OnAcceptSettingsClick()
        {
            _controllerSettingsPanelState = !_controllerSettingsPanelState;
            _view.SetControllerSettingsPanelState(_controllerSettingsPanelState);
        }
        
        private void OnKeyboardInputClick(bool value)
        {
            if (value == false) return;

            CharacterEventBus.Instance.OnCharacterMovementInputAction(new KeyboardInput());
        }
        
        private void OnTouchInputClick(bool value)
        {
            if (value == false) return;
            
            CharacterEventBus.Instance.OnCharacterMovementInputAction(new TouchInput());
        }

        private void OnTimerValueChange(float startValue, float currentValue)
        {
            _view.SetTimerValue(startValue, currentValue);
        } 
        
        private void OnScoreValueChange(int value)
        {
            _view.SetScoreValue(value);
        }
    }
}
