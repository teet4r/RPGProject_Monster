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

        _navMeshAgent.isStopped = false;
        _navMeshAgent.stoppingDistance = data.stoppingDistance;
        _navMeshAgent.speed = data.moveSpeed;
        _navMeshAgent.angularSpeed = 360f;
        _navMeshAgent.acceleration = 50f;
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

    #region Move Methods
    void _RushToTarget()
    {
        _navMeshAgent.stoppingDistance = data.stoppingDistance;
        _navMeshAgent.destination = target.transform.position;

        // 플레이어를 쳐다봄
        _LookAt(target.transform.position);
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
                _navMeshAgent.destination = Algorithms.GetRandomPointOnNavMesh(transform.position, Random.Range(0f, data.patrolDistance));
            }
            else yield return null;
        }
    }
    #endregion

    public bool hasTarget { get { return target != null && target.isAlive; } }
    public bool isRecognized // 플레이어가 시야에 들어올 때
    {
        get { return hasTarget && Vector3.Distance(target.transform.position, transform.position) <= data.recognitionDistance; }
    }

    public Player target = null; // 몬스터가 따라갈 대상
    public MonsterData data; // 몬스터 데이터 컨테이너

    protected Animator _animator = null;
    protected NavMeshAgent _navMeshAgent = null;
    protected Rigidbody _rigidbody = null;
    protected Coroutine _patrolCor = null;
}
