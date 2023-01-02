using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : NormalMonsterObject
{
    protected override void Awake()
    {
        base.Awake();

        _attackPatterns.Add(GetComponent<DragonPattern1>());
        _attackPatterns.Add(GetComponent<DragonPattern2>());
    }
    protected override IEnumerator _Attack()
    {
        isAttacking = true;

        int idx = Random.Range(0, _attackClips.Length);
        _animator.SetTrigger(AnimatorID.Trigger.Attacks[idx]);
        _attackPatterns[idx].Attack(target.transform);
        
        yield return new WaitForSeconds(_attackClips[idx].length + 1f);

        _navMeshAgent.destination = target.transform.position;
        isAttacking = false;
        _attackCor = null;
    }

    List<IAttackPattern> _attackPatterns = new List<IAttackPattern>();
}
