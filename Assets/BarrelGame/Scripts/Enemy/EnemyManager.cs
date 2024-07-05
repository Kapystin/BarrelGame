using BarrelGame.Scripts.Character;
using UnityEngine;

namespace BarrelGame.Scripts.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyDetectionSystem _enemyDetectionSystem;

        public void SetTarget(CharacterManager target)
        {
            _enemyDetectionSystem.SetTarget(target);
        }
    }
}