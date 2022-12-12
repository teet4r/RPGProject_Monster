using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// LifeObject�� ����ϴ� ���� Ŭ����
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

        // �÷��̾ �Ĵٺ�
        _LookAt(_player.transform.position);
    }

    IEnumerator _Patrol(Vector3 startPosition)
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = startPosition;
        _navMeshAgent.stoppingDistance = 0f;

        while (true)
        {
            // �������� ��
            if (_navMeshAgent.remainingDistance < 0.01f)
            {
                yield return new WaitForSeconds(Random.Range(0f, 7f));
                _navMeshAgent.destination = _GetRandomPointOnNavMesh(transform.position, Random.Range(0f, _patrolDistance));
            }
            else yield return null;
        }
    }

    // ����޽� ���� ������ ��ġ�� ��ȯ�ϴ� �޼���
    // center�� �߽����� distance �ݰ� �ȿ��� ������ ��ġ�� ã�´�
    Vector3 _GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center�� �߽����� �������� maxDistance�� �� �ȿ����� ������ ��ġ �ϳ��� ����
        // Random.insideUnitSphere�� �������� 1�� �� �ȿ����� ������ �� ���� ��ȯ�ϴ� ������Ƽ
        Vector3 randomPos = center + Random.insideUnitSphere * distance;

        // ����޽� ���ø��� ��� ������ �����ϴ� ����
        NavMeshHit hit;

        // distance �ݰ� �ȿ���, randomPos�� ���� ����� ����޽� ���� �� ���� ã��
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        // ã�� �� ��ȯ
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
