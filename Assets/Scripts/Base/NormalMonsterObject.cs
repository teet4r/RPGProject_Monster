using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonsterObject를 상속하는 하위 클래스
/// </summary>
public class NormalMonsterObject : MonsterObject
{
    protected override void _RushToTarget()
    {
        _navMeshAgent.stoppingDistance = data.stoppingDistance;
        _navMeshAgent.destination = target.transform.position;

        var lookDir2D = target.transform.position;
        lookDir2D.y = 0f;
        transform.LookAt(lookDir2D);

        if (isAttackable && _attackCor == null)
            _attackCor = StartCoroutine(_Attack());
    }
    protected override IEnumerator _Attack()
    {
        isAttacking = true;

        int idx = Random.Range(0, _attackClips.Length);
        _animator.SetTrigger(AnimatorID.Trigger.Attacks[idx]);
        yield return new WaitForSeconds(_attackClips[idx].length + 1f);

        _navMeshAgent.destination = target.transform.position;
        isAttacking = false;
        _attackCor = null;
    }
}
