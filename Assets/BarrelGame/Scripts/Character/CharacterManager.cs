using BarrelGame.Scripts.Enums;
using BarrelGame.Scripts.Interface;
using BarrelGame.Scripts.UI;
using BarrelGame.Scripts.UI.UIStates;
using UnityEngine;
using UnityEngine.Serialization;

namespace BarrelGame.Scripts.Character
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private CharacterAnimator _characterAnimator;

        [SerializeField] private GameObject _destructionBarrel;
        [SerializeField] private GameObject _mainBarrel;

        public bool IsHide => _isHide;
        
        private Joystick _joystick;
        private bool _isHide;
        
        private void OnEnable()
        {
            GameStateMachine.Instance.OnGameStateChange += OnGameStateChange;
            CharacterEventBus.Instance.OnCharacterMovementInput += OnCharacterMovementInput;
            CharacterEventBus.Instance.OnCharacterCaught += OnCharacterCaught;
        }

        private void OnDisable()
        {
            CharacterEventBus.Instance.OnCharacterMovementInput -= OnCharacterMovementInput;
            GameStateMachine.Instance.OnGameStateChange -= OnGameStateChange;
            CharacterEventBus.Instance.OnCharacterCaught -= OnCharacterCaught;
        }
        
        private void Update()
        {
            var normalizedVelocity = _characterController.velocity.normalized.magnitude;
            
            _isHide = normalizedVelocity < 0.01f;
        }
        
        public void SetupJoystick(Joystick joystick)
        {
            _joystick = joystick;
        }

        private void OnCharacterCaught()
        {
            _characterAnimator.OnCharacterCaught();
            _mainBarrel.SetActive(false);
            _destructionBarrel.SetActive(true);
        }
        
        private void OnCharacterMovementInput(IMovementInput movementInput)
        {
            if (movementInput is TouchInput touchInput)
            {
                touchInput.SetupJoystick(_joystick);
            }
            
            _characterMovement.SetMovementType(movementInput);
        }

        private void OnGameStateChange(GameStateType gameStateType)
        {
            _characterMovement.SetCurrentGameState(gameStateType);

            if (gameStateType is GameStateType.Win)
            {
                _characterAnimator.OnCharacterWin();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<FinishTag>())
            {
                GameStateMachine.Instance.SetState(GameStateType.Win);
                UIStateMachine.Instance.SetState(new WinUIState());
            }
            else if (other.GetComponent<MoneyAnimator>())
            {
                other.GetComponent<MoneyAnimator>().PlayTakeAnimation();
            }
        }

    }
}
