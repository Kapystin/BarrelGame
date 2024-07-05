using BarrelGame.Scripts.Character;
using BarrelGame.Scripts.Enums;
using BarrelGame.Scripts.Interface.UI;
using BarrelGame.Scripts.UI.UIStates;
using UnityEngine;

namespace BarrelGame.Scripts.UI.BriefingPanel
{
    public class BriefingPanelViewModel : IViewModel
    {
        private readonly BriefingPanelView _view;
        private readonly BriefingPanelModel _model;
        
        public BriefingPanelViewModel(BriefingPanelView view, BriefingPanelModel model)
        {
            _view = view;
            _model = model;
            
            CharacterEventBus.Instance.OnCharacterMovementInput(new KeyboardInput());
        }
        
        public void AddListeners()
        {
            _view.OnStartGameClick += OnStartGameClick;
            _view.OnKeyboardInputClick += OnKeyboardInputClick;
            _view.OnTouchInputClick += OnTouchInputClick;
        }

        public void RemoveListeners()
        {
            _view.OnStartGameClick -= OnStartGameClick;
            _view.OnKeyboardInputClick -= OnKeyboardInputClick;
            _view.OnTouchInputClick -= OnTouchInputClick;
        }

        private void OnStartGameClick()
        {
            GameStateMachine.Instance.SetState(GameStateType.Intro);
            UIStateMachine.Instance.SetState(new InGameUIState());
            _view.Destroy();
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
    }
}
