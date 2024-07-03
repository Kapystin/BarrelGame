using BarrelGame.Scripts.Character;
using UnityEngine;

namespace BarrelGame.Scripts.Barrel
{
    public class BarrelAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private const string _destruction = "destruction";

        private void OnEnable()
        {
            CharacterEventBus.Instance.OnCharacterCaught += OnCharacterDeath;
        }

        private void OnDisable()
        {
            CharacterEventBus.Instance.OnCharacterCaught -= OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            _animator.SetTrigger(_destruction);
        }
    }
}
