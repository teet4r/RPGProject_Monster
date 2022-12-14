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

        _animator.SetBool(AnimatorID.IsWalking, isWalking);
    }

    public virtual void Attack()
    {

    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);

        _animator.SetTrigger(AnimatorID.Hit);
    }

    protected override void Die()
    {
        base.Die();

        _boxCollider.enabled = false;
        _animator.SetTrigger(AnimatorID.Die);
        //gameObject.SetActive(false);
    }

    BoxCollider _boxCollider;
}
