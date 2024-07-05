using BarrelGame.Scripts.Character;

namespace BarrelGame.Scripts.Barrel
{
    public class DestructionBarrelAnimator : BaseBarrelAnimator
    {
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
