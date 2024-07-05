using System;
using BarrelGame.Scripts.Character;
using BarrelGame.Scripts.Enemy;
using BarrelGame.Scripts.Enums;
using BarrelGame.Scripts.Interface;
using BarrelGame.Scripts.ScriptableObjectData;
using Cinemachine;
using UnityEngine;

namespace BarrelGame.Scripts
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _characterVirtualCamera;
        [SerializeField] private CinemachineVirtualCamera _winVirtualCamera;
        
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private LevelData _levelData;

        [SerializeField] private VariableJoystick _variableJoystick;
        
        private EnemyEventBus _enemyEventBus;
        private CharacterEventBus _characterEventBus;
        private GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            _enemyEventBus = EnemyEventBus.Instance;
            _characterEventBus = CharacterEventBus.Instance;
            _gameStateMachine = GameStateMachine.Instance;

            var level = InstantiateLevel();
            var characterSpawnPosition = level.GetCharacterSpawnPosition();
            
            var characterManager = InstantiateCharacter(characterSpawnPosition);
            characterManager.SetupJoystick(_variableJoystick);
            
            _characterVirtualCamera.Follow = characterManager.transform;
            _winVirtualCamera.Follow = characterManager.transform;
            
            _characterVirtualCamera.gameObject.SetActive(true);
            
            level.SetEnemyTarget(characterManager);
        }

        private void OnEnable()
        {
            _gameStateMachine.OnGameStateChange += OnWinGame;
            _characterEventBus.OnCharacterMovementInput += OnCharacterMovementInput;
        }

        private void OnDisable()
        {
            _gameStateMachine.OnGameStateChange -= OnWinGame;
            CharacterEventBus.Instance.OnCharacterMovementInput -= OnCharacterMovementInput;
        }

        private void OnCharacterMovementInput(IMovementInput movementInput)
        {
            _variableJoystick.gameObject.SetActive(movementInput is TouchInput);
        }
        
        private LevelManager InstantiateLevel()
        {
            return Instantiate(_levelData.GetRandomLevelManager()); 
        }
        
        private CharacterManager InstantiateCharacter(Transform position)
        {
            return Instantiate(_characterData.characterManagerPrefab, position);
        }

        private void OnWinGame(GameStateType gameStateType)
        {
            if (gameStateType != GameStateType.Win) return;
            
            _characterVirtualCamera.gameObject.SetActive(false);
            _winVirtualCamera.gameObject.SetActive(true);
        }
    }
}
