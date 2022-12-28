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
    protected override void _Die()
    {
        base._Die();

        //gameObject.SetActive(false);
    }
    protected override IEnumerator _Attack()
    {
        // �÷��̾� �ڸ����� ȸ��
        // ���� ��� ����
        isAttacking = true;
        _navMeshAgent.isStopped = true;
        yield return _rotate3D.StartCoroutine(_rotate3D.Rotate(target.transform.position));

        // ������ ������̱� ������ 0.1�ʰ� �� ���� �� ����(�÷��̾ ���� ������ �ֵ���)
        var targetPos = target.transform.position;
        yield return _wfs_readyToAttack;

        // ����
        int idx = Random.Range(0, _attackClips.Length);
        _animator.SetTrigger(AnimatorID.Trigger.Attacks[idx]);
        _attackPatterns[idx].Attack(targetPos);

        // ���� �ִϸ��̼� + 1�ʰ� ���� ������ ���
        yield return new WaitForSeconds(_attackClips[idx].length + 1f);

        // ���� ����ġ
        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = target.transform.position;
        isAttacking = false;
        _attackCor = null;
    }

    List<IAttackPattern> _attackPatterns = new List<IAttackPattern>();
    WaitForSeconds _wfs_readyToAttack = new WaitForSeconds(0.1f);
}
