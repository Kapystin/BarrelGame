using System;
using UnityEngine;

namespace BarrelGame.Scripts.Enemy
{
    public class EnemyAnimatorWithRevolver : EnemyBaseAnimator
    {
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyDetectionSystem _enemyDetectionSystem;
        
        private const string _velocity = "velocity";
        private const string _shoot = "shoot";

        private void OnEnable()
        {
            _enemyDetectionSystem.OnTargetDetected += OnEnemyDetected;
        }

        private void OnDisable()
        {
            _enemyDetectionSystem.OnTargetDetected -= OnEnemyDetected; 
        }

        private void Update()
        {
            var velocity = _enemyMovement.Velocity;
            
            SetVelocityValue(velocity);
        }

        private void SetVelocityValue(float value)
        {
            _animator.SetFloat(_velocity, value);
        }

        private void OnEnemyDetected()
        {
            _animator.SetTrigger(_shoot);
        }
    }
}
