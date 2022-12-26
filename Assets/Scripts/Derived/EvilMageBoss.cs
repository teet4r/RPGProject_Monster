using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMageBoss : BossMonsterObject
{
    protected override void _Die()
    {
        base._Die();

        //gameObject.SetActive(false);
    }
    protected override IEnumerator _Attack()
    {
        // 플레이어 자리 쳐다보기
        isAttacking = true;
        _navMeshAgent.isStopped = true;
        yield return _rotate3D.StartCoroutine(_rotate3D.Rotate(target.transform.position, 0.5f));

        int idx = Random.Range(0, _attackClips.Length);
        _animator.SetTrigger(AnimatorID.Trigger.Attacks[idx]);
        StartCoroutine(_PlayEffect(idx, target.transform.position));

        yield return new WaitForSeconds(_attackClips[idx].length + 1f);

        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = target.transform.position;
        isAttacking = false;
        _attackCor = null;
    }
    IEnumerator _PlayEffect(int attackMotionIndex, Vector3 position)
    {
        yield return new WaitForSeconds(1f);
        effects[attackMotionIndex].transform.position = position;
        effects[attackMotionIndex].Play();
        yield return new WaitForSeconds(1f);
        effects[attackMotionIndex].Stop(true);
    }
    
    public ParticleSystem[] effects;
}
