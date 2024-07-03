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


        private void OnEnable()
        {
            EnemyEventBus.Instance.OnCharacterDetect += OnPlayDeath;
        }

        private void OnDisable()
        {
            EnemyEventBus.Instance.OnCharacterDetect -= OnPlayDeath;
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

        private void OnPlayDeath()
        {
            
        }
    }
}
