using System;
using UnityEngine;

namespace BarrelGame.Scripts.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private const string _velocity = "velocity";
        
        private void Update()
        {
            // Debug.Log($">>>{_characterController.velocity.magnitude}");

            SetVelocityValue(_characterController.velocity.normalized.magnitude);
        }

        private void SetVelocityValue(float value)
        {
            Debug.Log($">>>velocity.magnitude: {value}");
            _animator.SetFloat(_velocity, value);
        }
    }
}
