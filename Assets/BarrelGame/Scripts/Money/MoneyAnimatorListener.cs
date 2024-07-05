using BarrelGame.Scripts.Character;
using UnityEngine;

namespace BarrelGame.Scripts.Money
{
    public class MoneyAnimatorListener : MonoBehaviour
    {
        [SerializeField] private GameObject _moneyParent;
        
        public void DisableParentObject()
        {
            _moneyParent.SetActive(false);
        }
        
        public void OnMoneyTake()
        {
            CharacterEventBus.Instance.OnCollectablesTakeAction();
        }
    }
}
