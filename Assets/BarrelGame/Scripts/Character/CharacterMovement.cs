using System;
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

        private void Start()
        {
            SetMovementType(new KeyboardInput());
        }

        private void Update()
        {
            Move(Time.deltaTime);
        }

        private void SetMovementType(IMovementInput movementInput)
        {
            _movementInput = movementInput;
        }

        private void Move(float deltaTime)
        {
            var movementInput = _movementInput.GetMovementInput().MovementInput;

            // if (movementInput == Vector3.zero) return;

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
