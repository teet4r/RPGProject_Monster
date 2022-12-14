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

        _prevAttackTime = 0f;
    }

    protected override void Update()
    {
        base.Update();

        _animator.SetBool(AnimatorID.Bool.IsWalking, isWalking);
        if (hasTarget &&
            Vector3.Distance(target.transform.position, transform.position) <= data.stoppingDistance + 1f &&
            Time.time - _prevAttackTime >= _attackRate)
        {
            _prevAttackTime = Time.time;
            _animator.SetInteger(AnimatorID.Int.Attack, Random.Range(0, _attackMotionCount));
        }
        else
            _animator.SetInteger(AnimatorID.Int.Attack, -1);
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);

        _animator.SetTrigger(AnimatorID.Trigger.Hit);
    }

    protected override void Die()
    {
        base.Die();

        _sphereCollider.enabled = false;
        _animator.SetTrigger(AnimatorID.Trigger.Die);
        //gameObject.SetActive(false);
    }

    [SerializeField]
    float _attackRate = 1f;

    SphereCollider _sphereCollider;
    float _prevAttackTime = 0f;
    readonly int _attackMotionCount = 2;
}
