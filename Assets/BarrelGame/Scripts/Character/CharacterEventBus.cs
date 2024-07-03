using System;
using UnityEngine;

namespace BarrelGame.Scripts.Character
{
    public class CharacterEventBus 
    {
        public static CharacterEventBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharacterEventBus();
                }

                return _instance;
            }
        }
        
        private static CharacterEventBus _instance = null;

        public Action OnCharacterCaught;

        public void OnCharacterDeathAction()
        {
            OnCharacterCaught?.Invoke();
        }
    }
}
