using UnityEngine;

namespace BarrelGame.Scripts.Enemy
{
    public abstract class EnemyBaseAnimator : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
    }
}
