using System;
using BarrelGame.Scripts.Enums;
using BarrelGame.Scripts.Interface;
using BarrelGame.Scripts.UI;
using BarrelGame.Scripts.UI.UIStates;
using UnityEngine;

namespace BarrelGame.Scripts.Character
{
    public class CharacterEventBus 
    {
        public static CharacterEventBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharacterEventBus();
                }

                return _instance;
            }
        }
        
        private static CharacterEventBus _instance = null;

        public Action OnCharacterCaught;
        public Action<IMovementInput> OnCharacterMovementInput;
        public Action OnCollectablesTake;
        public Action OnCharacterInitDone;
        public Action OnCharacterBarrelPickup;

        public void OnCharacterDeathAction()
        {
            UIStateMachine.Instance.SetState(new LoseUIState());
            GameStateMachine.Instance.SetState(GameStateType.Lose);
            OnCharacterCaught?.Invoke();
        }

        public void OnCharacterMovementInputAction(IMovementInput movementInput)
        {
            OnCharacterMovementInput?.Invoke(movementInput);
        }

        public void OnCollectablesTakeAction()
        {
            OnCollectablesTake?.Invoke();
        }

        public void OnCharacterInitDoneAction()
        {
            GameStateMachine.Instance.SetState(GameStateType.Play);
            OnCharacterInitDone?.Invoke();
        }
        
        public void OnCharacterPickupBarrelAction()
        {
            OnCharacterBarrelPickup?.Invoke();
        }
    }
}
