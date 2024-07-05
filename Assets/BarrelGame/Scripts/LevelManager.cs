using BarrelGame.Scripts.Character;
using BarrelGame.Scripts.Enemy;
using BarrelGame.Scripts.Enums;
using UnityEngine;

namespace BarrelGame.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform _characterSpawnPosition;
        [SerializeField] private EnemyManager[] _enemyManagers;
        [SerializeField] private ParticleSystem[] _finishStarsParticles;
        
        private void OnEnable()
        {
            GameStateMachine.Instance.OnGameStateChange += OnWinGame;
        }

        private void OnDisable()
        {
            GameStateMachine.Instance.OnGameStateChange -= OnWinGame;
        }

        public Transform GetCharacterSpawnPosition()
        {
            return _characterSpawnPosition;
        }

        public void SetEnemyTarget(CharacterManager target)
        {
            foreach (var enemy in _enemyManagers)
            {
                enemy.SetTarget(target);
            }
        }
        
        private void OnWinGame(GameStateType gameStateType)
        {
            if (gameStateType != GameStateType.Win) return;

            foreach (var particleSystem in _finishStarsParticles)
            {
                particleSystem.Play();
            }
        }
    }
}
