using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMageBoss : BossMonsterObject
{
    protected override void Awake()
    {
        base.Awake();

        _attackPatterns.Add(GetComponent<EvilMagePattern1>());
        _attackPatterns.Add(GetComponent<EvilMagePattern2>());
    }
    protected override IEnumerator _Attack()
    {
        // �÷��̾� �ڸ����� ȸ��
        // ���� ��� ����
        isAttacking = true;
        _navMeshAgent.isStopped = true;
        yield return _rotate3D.StartCoroutine(_rotate3D.Rotate(target.transform.position));

        // ����
        int idx = Random.Range(0, _attackClips.Length);
        _animator.SetTrigger(AnimatorID.Trigger.Attacks[idx]);
        _attackPatterns[idx].Attack(target.transform);

        // ���� �ִϸ��̼� + 1�ʰ� ���� ������ ���
        yield return new WaitForSeconds(_attackClips[idx].length + 1f);

        // ���� ����ġ
        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = target.transform.position;
        isAttacking = false;
        _attackCor = null;
    }

    List<IAttackPattern> _attackPatterns = new List<IAttackPattern>();
}
