using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChestMonster : MonsterObject
{
    protected override void Awake()
    {
        base.Awake();

        _boxCollider = GetComponent<BoxCollider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _boxCollider.enabled = true;
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

        _boxCollider.enabled = false;
        _animator.SetTrigger(AnimatorID.Trigger.Die);
        //gameObject.SetActive(false);
    }

    BoxCollider _boxCollider;
}
