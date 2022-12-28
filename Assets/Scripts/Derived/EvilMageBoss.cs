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
        // 플레이어 자리까지 회전
        // 상태 잠시 변경
        isAttacking = true;
        _navMeshAgent.isStopped = true;
        yield return _rotate3D.StartCoroutine(_rotate3D.Rotate(target.transform.position));

        // 공격이 즉발형이기 때문에 0.1초간 멍 때린 후 공격(플레이어가 피할 여지를 주도록)
        var targetPos = target.transform.position;
        yield return _wfs_readyToAttack;

        // 공격
        int idx = Random.Range(0, _attackClips.Length);
        _animator.SetTrigger(AnimatorID.Trigger.Attacks[idx]);
        _attackPatterns[idx].Attack(targetPos);

        // 공격 애니메이션 + 1초가 끝날 때까지 대기
        yield return new WaitForSeconds(_attackClips[idx].length + 1f);

        // 상태 원위치
        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = target.transform.position;
        isAttacking = false;
        _attackCor = null;
    }

    List<IAttackPattern> _attackPatterns = new List<IAttackPattern>();
    WaitForSeconds _wfs_readyToAttack = new WaitForSeconds(0.1f);
}
