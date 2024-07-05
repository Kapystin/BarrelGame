using BarrelGame.Scripts.Character;
using BarrelGame.Scripts.Enums;
using UnityEngine;

namespace BarrelGame.Scripts.Barrel
{
    public class MainBarrelAnimator : BaseBarrelAnimator
    {
        [SerializeField] private CharacterController _characterController;
        private const string _velocity = "velocity";
        private const string _win = "win";
        private const string _init = "init";
        
        private void OnEnable()
        {
            CharacterEventBus.Instance.OnCharacterBarrelPickup += OnCharacterBarrelPickup;
            GameStateMachine.Instance.OnGameStateChange += OnGameStateChange;
            
        }

        private void OnDisable()
        {
            CharacterEventBus.Instance.OnCharacterBarrelPickup -= OnCharacterBarrelPickup;
            GameStateMachine.Instance.OnGameStateChange -= OnGameStateChange;
        }

        private void Update()
        {
            var normalizedVelocity = _characterController.velocity.normalized.magnitude;
            
            SetVelocityValue(normalizedVelocity);
        }
        
        private void OnGameStateChange(GameStateType gameStateType)
        {
            if (gameStateType is GameStateType.Win)
            {
                _animator.SetTrigger(_win);
            }
        }

        private void SetVelocityValue(float value)
        {
            _animator.SetFloat(_velocity, value);
        }
        
        private void OnCharacterBarrelPickup()
        {
            _animator.SetTrigger(_init);
        }
    }
}