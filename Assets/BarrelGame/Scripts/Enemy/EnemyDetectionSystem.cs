using System;
using BarrelGame.Scripts.Character;
using UnityEngine;

namespace BarrelGame.Scripts.Enemy
{
    public class EnemyDetectionSystem : MonoBehaviour
    {
        public Action OnTargetDetected;
        
        [Range(0, 1)]
        [SerializeField] private float _detectionAngle = 0.1f;
        [Range(0, 10)]
        [SerializeField] private float _detectionRange = 5f;
        
        private CharacterManager _target;

        private void Update()
        {
            if (IsTargetDetected() == false) return;
            
            OnTargetDetected?.Invoke();            
            EnemyEventBus.Instance.OnCharacterDetectAction();
            transform.LookAt(_target.transform);
            enabled = false;
        }
        
        public void SetTarget(CharacterManager target)
        {
            _target = target;
        }

        private bool IsTargetDetected()
        {
            if (_target == null || _target.IsHide) return false;
            
            var directionToTarget = (_target.transform.position - transform.position).normalized;
            
            var dotProduct = Vector3.Dot(transform.forward, directionToTarget);
            var distanceToTarget = (transform.position - _target.transform.position).magnitude;

            var value = dotProduct > _detectionAngle && distanceToTarget < _detectionRange;
            return value;
        }
    }
}
