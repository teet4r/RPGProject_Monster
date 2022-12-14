using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BlackKnight : MonsterObject
{
    protected override void Awake()
    {
        base.Awake();

        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _capsuleCollider.enabled = true;
    }

    protected override void Update()
    {
        base.Update();

        _animator.SetBool(AnimatorID.Bool.IsWalking, isWalking);
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);

        _animator.SetTrigger(AnimatorID.Trigger.Hit);
    }

    protected override void Die()
    {
        base.Die();

        _capsuleCollider.enabled = false;
        _animator.SetTrigger(AnimatorID.Trigger.Die);
        //gameObject.SetActive(false);
    }

    CapsuleCollider _capsuleCollider;
}
