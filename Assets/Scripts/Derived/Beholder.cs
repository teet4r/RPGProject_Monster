using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Beholder : MonsterObject
{
    protected override void Awake()
    {
        base.Awake();

        _sphereCollider = GetComponent<SphereCollider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _sphereCollider.enabled = true;
    }

    protected override void Update()
    {
        base.Update();

        _animator.SetBool(AnimatorID.IsWalking, isWalking);
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);

        _animator.SetTrigger(AnimatorID.Hit);
    }

    protected override void Die()
    {
        base.Die();

        _sphereCollider.enabled = false;
        _animator.SetTrigger(AnimatorID.Die);
        //gameObject.SetActive(false);
    }

    SphereCollider _sphereCollider;
}
