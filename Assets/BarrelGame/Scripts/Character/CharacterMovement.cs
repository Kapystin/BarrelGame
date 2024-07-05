using System;
using BarrelGame.Scripts.Enums;
using BarrelGame.Scripts.Interface;
using UnityEngine;

namespace BarrelGame.Scripts.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        
        [Range(0, 10)] 
        [SerializeField] private float _moveSpeed = 5; 
        [Range(0, 10)] 
        [SerializeField] private float _rotationSpeed = 5; 
        
        private IMovementInput _movementInput;
        private GameStateType _gameStateType;

        private void Update()
        {
            if (_gameStateType != GameStateType.Play) return;
            
            Move(Time.deltaTime);
        }

        public void SetMovementType(IMovementInput movementInput)
        {
            _movementInput = movementInput;
        }
        
        public void SetCurrentGameState(GameStateType gameStateType)
        {
            _gameStateType = gameStateType;
        }

        private void Move(float deltaTime)
        {
            var movementInput = _movementInput.GetMovementInput().MovementInput;

            if (movementInput != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(movementInput, Vector3.up);
                _characterController.transform.rotation =
                    Quaternion.RotateTowards(_characterController.transform.rotation, targetRotation, _rotationSpeed);
            }

            movementInput = movementInput.normalized;
            _characterController.Move(movementInput * _moveSpeed * deltaTime);
        }

    }
}
