using System;
using UnityEngine;

namespace BarrelGame.Scripts.Enemy
{
    public class EnemyDetectionSystem : MonoBehaviour
    {
        public Action OnTargetDetected;
            
        [SerializeField] private Transform _target;
        [Range(0, 1)]
        [SerializeField] private float _detectionAngle = 0.1f;
        [Range(0, 10)]
        [SerializeField] private float _detectionRange = 5f; 
        
        

        private void Update()
        {
            if (IsTargetDetected() == false) return;
            
            OnTargetDetected?.Invoke();
            // EnemyEventBus.Instance.OnCharacterDetect?.Invoke();
            EnemyEventBus.Instance.OnCharacterDetectAction();
            
            this.enabled = false;
        }

        private bool IsTargetDetected()
        {
            var directionToTarget = (_target.position - transform.position).normalized;
            
            var dotProduct = Vector3.Dot(transform.forward, directionToTarget);
            var distanceToTarget = (transform.position - _target.position).magnitude;

            var value = dotProduct > _detectionAngle && distanceToTarget < _detectionRange;
            return value;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_target == null) return;
            DrawLines();
        }
        
        private void DrawLines()
        {
            if (IsTargetDetected() == false ) return;
            
            Gizmos.color = Color.red;
            
            Gizmos.DrawLine(transform.position, _target.position);
        }
#endif
    }
}
