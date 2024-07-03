using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace BarrelGame.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public float Velocity => _velocity;

        [SerializeField] private EnemyDetectionSystem _enemyDetectionSystem;
        [Header("Await time at point in seconds")]
        [SerializeField] private float _delayTimeAtPoint = 3f;
        [SerializeField] private bool _isCyclicPath;
        [SerializeField] private Transform[] _pathPoints;

        [Range(0, 10)]
        [SerializeField] private float _moveSpeed = 0.7f;
        
        [Space] 
        [Header("Gizmos Settings")]
        [SerializeField] private float _pointRadius;
        [SerializeField] private Color _pointColor;
        [SerializeField] private Color _lineColor;
        
        private Vector3 _movement;
        private float _velocity;
        
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        
        private void OnEnable()
        {
            StartMove();

            _enemyDetectionSystem.OnTargetDetected += OnTargetDetected;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        private void OnDisable()
        {
            _enemyDetectionSystem.OnTargetDetected += OnTargetDetected;
            
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private void OnTargetDetected()
        {
            _cancellationTokenSource.Cancel();
        }
        
        private float GetVelocity(Vector3 point)
        {
            if (_cancellationToken.IsCancellationRequested) return 0;

            var position = transform.position;
            var positionDelta = new Vector3(
                Math.Abs(point.x - position.x),
                Math.Abs(point.y - position.y),
                Math.Abs(point.z - position.z)
                );
            var velocity = positionDelta.normalized.magnitude;
            
            if (Vector3.Distance(transform.position, point) < 0.03)
            {
                velocity = 0f;
            }
            return velocity;
        }
        
        private async UniTask StartMove()
        {
            if (_pathPoints.Length <= 0)
            {
                Debug.LogError($"No path points at Enemy [{gameObject.name}]!");
                return;
            }

            var nextPoint = GetNearestPathPoint();

            await MoveToPoint(nextPoint);
            
            await UniTask.WaitForSeconds(_delayTimeAtPoint, cancellationToken: _cancellationToken);
            
            while(_pathPoints.Length > 1 && _isCyclicPath)
            {
                if (_cancellationToken.IsCancellationRequested)
                    break;
                
                nextPoint = GetNextPathPoint();
                
                await MoveToPoint(nextPoint);
                
                _velocity = GetVelocity(nextPoint);
                
                await UniTask.WaitForSeconds(_delayTimeAtPoint, cancellationToken: _cancellationToken);
            }
        }

        private async UniTask MoveToPoint(Vector3 point)
        {
            await UniTask.WaitUntil(() =>
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    return true;
                }

                var position = transform.position;
                var movement = Vector3.MoveTowards(position, point, _moveSpeed * Time.deltaTime);
                
                transform.LookAt(movement);
                _velocity = GetVelocity(point);
                
                transform.position = movement;
                
                return Vector3.Distance(transform.position, point) < 0.01;
            });
        }

        private Vector3 GetNextPathPoint()
        {
            var currentPointIndex = GetCurrentPathPointIndex();
            var nextPointIndex = 0;

            nextPointIndex = currentPointIndex + 1;

            if (nextPointIndex >= _pathPoints.Length || nextPointIndex < 0)
            {
                nextPointIndex = 0;
            }
            
            return _pathPoints[nextPointIndex].position;
        }

        private int GetCurrentPathPointIndex()
        {
            var currentPoint = GetNearestPathPoint();
            
            for (int i = 0; i < _pathPoints.Length; i++)
            {
                var point = _pathPoints[i].position;
                
                if (Vector3.Distance(currentPoint, point) < 0.1)
                {
                    return i;
                }
            }

            return 0;
        }
        
        private Vector3 GetNearestPathPoint()
        {
            var nearestPoint = _pathPoints[0].position;
            var distanceToEnemy = Vector3.Distance(transform.position, nearestPoint);

            foreach (var point in _pathPoints)
            {
                if (Vector3.Distance(transform.position, point.position) < distanceToEnemy)
                {
                    nearestPoint = point.position;
                    distanceToEnemy = Vector3.Distance(transform.position, nearestPoint);
                }
            }
            
            return nearestPoint;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_pathPoints.Length <= 0) return;

            DrawPoints();
            DrawLines();
        }

        private void DrawPoints()
        {
            Gizmos.color = _pointColor;
            foreach (var point in _pathPoints)
            {
                Gizmos.DrawSphere(point.transform.position, _pointRadius);
            }
        }
        
        private void DrawLines()
        {
            Gizmos.color = _lineColor;
            
            Gizmos.DrawLine(_pathPoints[0].transform.position, _pathPoints[1].transform.position);
        }
#endif
    }
}
