using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightBoss : BossMonsterObject
{
    protected override void Awake()
    {
        base.Awake();

        _attackPatterns.Add(GetComponent<BlackKnightPattern1>());
    }
    protected override IEnumerator _Attack()
    {
        // �÷��̾� �ڸ� �Ĵٺ���
        isAttacking = true;
        _navMeshAgent.isStopped = true;
        yield return _rotate3D.StartCoroutine(_rotate3D.Rotate(target.transform.position));

        // ����
        int idx = Random.Range(0, _attackClips.Length);
        _animator.SetTrigger(AnimatorID.Trigger.Attacks[idx]);
        _attackPatterns[idx].Attack(hasTarget ? target.transform : null);

        // ���� �ִϸ��̼� + 1�ʰ� ���� ������ ���
        yield return new WaitForSeconds(_attackClips[idx].length + 1f);

        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = hasTarget ? target.transform.position : transform.position;
        isAttacking = false;
        _attackCor = null;
    }

    List<IAttackPattern> _attackPatterns = new List<IAttackPattern>();
}
