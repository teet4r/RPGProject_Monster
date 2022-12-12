using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// LifeObject를 상속하는 하위 클래스
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class MonsterObject : LifeObject
{
    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator= GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        #region Set NavMeshAgent
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = 7f;
        _navMeshAgent.angularSpeed = 360f;
        _navMeshAgent.acceleration = 50f;
        #endregion
    }

    protected override void Update()
    {
        base.Update();

        if (isRecognized)
        {
            if (_patrolCor != null)
            {
                StopCoroutine(_patrolCor);
                _patrolCor = null;
            }
            _RushToTarget();
        }
        else
        {
            if (_patrolCor == null)
                _patrolCor = StartCoroutine(_Patrol(transform.position));
        }
    }

    protected override void Die()
    {
        base.Die();

        _patrolCor = null;
    }

    protected void _LookAt(Vector3 target)
    {
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
    }

    void _RushToTarget()
    {
        _navMeshAgent.stoppingDistance = 3f;
        _navMeshAgent.destination = _player.transform.position;

        // 플레이어를 쳐다봄
        _LookAt(_player.transform.position);
    }

    IEnumerator _Patrol(Vector3 startPosition)
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = startPosition;
        _navMeshAgent.stoppingDistance = 0f;

        while (true)
        {
            // 도착했을 때
            if (_navMeshAgent.remainingDistance < 0.01f)
            {
                yield return new WaitForSeconds(Random.Range(0f, 7f));
                _navMeshAgent.destination = _GetRandomPointOnNavMesh(transform.position, Random.Range(0f, _patrolDistance));
            }
            else yield return null;
        }
    }

    // 내비메시 위의 랜덤한 위치를 반환하는 메서드
    // center를 중심으로 distance 반경 안에서 랜덤한 위치를 찾는다
    Vector3 _GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center를 중심으로 반지름이 maxDistance인 구 안에서의 랜덤한 위치 하나를 저장
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = center + Random.insideUnitSphere * distance;

        // 내비메시 샘플링의 결과 정보를 저장하는 변수
        NavMeshHit hit;

        // distance 반경 안에서, randomPos에 가장 가까운 내비메시 위의 한 점을 찾음
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        // 찾은 점 반환
        return hit.position;
    }

    public bool hasTarget { get { return _player != null && _player.isAlive; } }
    public bool isRecognized
    {
        get { return hasTarget && Vector3.Distance(_player.transform.position, transform.position) <= _recognitionDistance; }
    }

    [Header("---Target---")]
    [SerializeField]
    protected Player _player = null;
    [Header("---Variables---")]
    [SerializeField]
    protected float _patrolDistance = 30f;
    [SerializeField]
    protected float _recognitionDistance = 20f;

    protected Animator _animator = null;
    protected NavMeshAgent _navMeshAgent = null;
    protected Rigidbody _rigidbody = null;
    protected Coroutine _patrolCor = null;
}
