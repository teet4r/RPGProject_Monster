using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// LifeObject를 상속하는 하위 클래스
/// </summary>
#region Require Components
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
#endregion
public abstract class MonsterObject : LifeObject
{
    protected override void Awake()
    {
        base.Awake();

        _bodyCollider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator= GetComponent<Animator>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        isRecognized = false;
        isAttackable = false;
        isAttacking = false;

        _bodyCollider.isTrigger = true;
        _bodyCollider.enabled = true;

        _patrolCor = null;
        _attackCor = null;

        _prevAttackTime = 0f;

        _navMeshAgent.isStopped = false;
        _navMeshAgent.stoppingDistance = data.stoppingDistance;
        _navMeshAgent.autoBraking = false;
        _navMeshAgent.speed = data.moveSpeed;
        _navMeshAgent.angularSpeed = 360f;
        _navMeshAgent.acceleration = 50f;
    }
    protected override void Update()
    {
        base.Update();

        target = GameManager.instance.player;

        _Move();

        _animator.SetBool(AnimatorID.Bool.IsWalking, isWalking);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            GetDamage(25f);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            player.GetDamage(data.damage);
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);

        _animator.SetTrigger(AnimatorID.Trigger.Hit);
    }
    protected override void _UpdateStates()
    {
        base._UpdateStates();

        isRecognized = hasTarget && Vector3.Distance(target.transform.position, transform.position) <= data.recognitionDistance;
        if (hasTarget &&
            Vector3.Distance(target.transform.position, transform.position) <= data.stoppingDistance + 1f &&
            Time.time - _prevAttackTime >= data.attackRate)
        {
            isAttackable = true;
            _prevAttackTime = Time.time;
        }
        else
            isAttackable = false;
    }
    protected override void _Die()
    {
        base._Die();

        isRecognized = false;
        isAttackable = false;

        _navMeshAgent.isStopped = true;
        if (_patrolCor != null)
        {
            StopCoroutine(_patrolCor);
            _patrolCor = null;
        }
        _bodyCollider.enabled = false;

        StartCoroutine(_DieRoutine());
    }
    protected virtual void _Move()
    {
        if (!isAlive) return;

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
    protected virtual IEnumerator _Patrol(Vector3 startPosition)
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
                _navMeshAgent.destination = Algorithm.GetRandomPointOnNavMesh(transform.position, Random.Range(0f, data.patrolDistance));
            }
            else yield return null;
        }
    }
    protected abstract void _RushToTarget();
    protected abstract IEnumerator _Attack();
    IEnumerator _DieRoutine()
    {
        _animator.SetTrigger(AnimatorID.Trigger.Die);
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }

    public Player target { get; protected set; } = null; // 몬스터가 따라갈 대상
    public bool hasTarget { get { return target != null && target.isAlive; } }
    public bool isRecognized { get; protected set; } // 플레이어가 시야에 들어올 때
    public bool isAttackable { get; protected set; } // 공격 가능할 때
    public bool isAttacking { get; protected set; } // 공격 중일 때 true
    public MonsterData data = null; // 몬스터 데이터 컨테이너
    [SerializeField] protected AnimationClip[] _attackClips;
    [SerializeField] protected float _destroyTime = 5f;
    protected Collider _bodyCollider = null;
    protected Animator _animator = null;
    protected NavMeshAgent _navMeshAgent = null;
    protected Rigidbody _rigidbody = null;
    protected Coroutine _patrolCor = null;
    protected Coroutine _attackCor = null;
    float _prevAttackTime = 0f;
}
