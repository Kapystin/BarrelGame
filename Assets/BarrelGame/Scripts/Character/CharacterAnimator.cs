using System;
using BarrelGame.Scripts.Enemy;
using BarrelGame.Scripts.Enums;
using UnityEngine;

namespace BarrelGame.Scripts.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private const string _velocity = "velocity";
        private const string _caught = "caught";
        private const string _dance = "dance";
        private const string _wave = "wave";
        private const string _init = "init";


        private void OnEnable()
        {
            GameStateMachine.Instance.OnGameStateChange += OnGameStateChange;
        }


        private void OnDisable()
        {
            GameStateMachine.Instance.OnGameStateChange -= OnGameStateChange;
        }
        
        private void Update()
        {
            var normalizedVelocity = _characterController.velocity.normalized.magnitude;
            
            SetVelocityValue(normalizedVelocity);
        }

        private void SetVelocityValue(float value)
        {
            _animator.SetFloat(_velocity, value);
        }

        public void OnCharacterCaught()
        {
            _animator.SetTrigger(_caught);
        }

        public void OnCharacterWin()
        {
            _animator.SetTrigger(_dance);
        }
        
        private void OnGameStateChange(GameStateType gameStateType)
        {
            if (gameStateType == GameStateType.Intro)
            {
                _animator.SetTrigger(_init);
            }
        }
    }
}
