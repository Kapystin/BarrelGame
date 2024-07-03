using System;
using UnityEngine;

namespace BarrelGame.Scripts.Enemy
{
    public class EnemyEventBus 
    {
        public static EnemyEventBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EnemyEventBus();
                }

                return _instance;
            }
        }

        private static EnemyEventBus _instance = null;

        public Action OnCharacterDetect;

        public void OnCharacterDetectAction()
        {
            OnCharacterDetect?.Invoke();
        }
    }
}
