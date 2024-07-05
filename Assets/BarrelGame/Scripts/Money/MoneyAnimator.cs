using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string _take = "take";

    public void PlayTakeAnimation()
    {
        _animator.SetTrigger(_take);
    }
}
