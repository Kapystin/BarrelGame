using BarrelGame.Scripts.Character;
using BarrelGame.Scripts.Enemy;
using UnityEngine;

namespace BarrelGame.Scripts
{
    public class Boot : MonoBehaviour
    {
        private EnemyEventBus _enemyEventBus;
        private CharacterEventBus _characterEventBus;
        
        private void Awake()
        {
            _enemyEventBus = EnemyEventBus.Instance;
            _characterEventBus = CharacterEventBus.Instance;
        }
    }
}
