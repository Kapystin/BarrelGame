using BarrelGame.Scripts.Character;
using UnityEngine;

namespace BarrelGame.Scripts.Enemy
{
    public class EnemyAnimationListener : MonoBehaviour
    {

        //This method call inside of animation RIg_middle_Shoot_Middle
        public void OnShootAnimation()
        {
            CharacterEventBus.Instance.OnCharacterDeathAction();
        }
    }
}
