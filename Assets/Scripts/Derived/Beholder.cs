using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : MonsterObject
{
    protected override void Update()
    {
        base.Update();

        _animator.SetBool(aid_isWalking, isWalking);
    }

    protected override void Die()
    {
        base.Die();

        gameObject.SetActive(false);
    }

    #region Animator ID
    readonly int aid_isWalking = Animator.StringToHash("IsWalking");
    #endregion
}
