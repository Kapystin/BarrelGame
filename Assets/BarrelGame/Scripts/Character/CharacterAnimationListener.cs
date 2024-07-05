using UnityEngine;

namespace BarrelGame.Scripts.Character
{
    public class CharacterAnimationListener : MonoBehaviour
    {
        public void OnPickupBarrel()
        {
            CharacterEventBus.Instance.OnCharacterPickupBarrelAction();
        }  
        
        public void OnCharacterInitDone()
        {
            CharacterEventBus.Instance.OnCharacterInitDoneAction();
        }
    }
}