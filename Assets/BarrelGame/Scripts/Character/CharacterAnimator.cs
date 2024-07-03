using System;
using BarrelGame.Scripts.Enemy;
using UnityEngine;

namespace BarrelGame.Scripts.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private const string _velocity = "velocity";
        private const string _caught = "caught";


        private void OnEnable()
        {
            CharacterEventBus.Instance.OnCharacterCaught += OnCharacterCaught;
        }

        private void OnDisable()
        {
            CharacterEventBus.Instance.OnCharacterCaught -= OnCharacterCaught;
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

        private void OnCharacterCaught()
        {
            _animator.SetTrigger(_caught);
        }
    }
}
