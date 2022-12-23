using System.Collections;
using System.Collections.Generic;
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
[RequireComponent(typeof(Rotate3D))]
#endregion
public class MonsterObject : LifeObject
{
    #region Unity Messages
    protected virtual void Awake()
    {
        _bodyCollider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator= GetComponent<Animator>();
        _rotate3D = GetComponent<Rotate3D>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        hasTarget = false;
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

        _Move();

        _animator.SetBool(AnimatorID.Bool.IsWalking, isWalking);
    }
    #endregion
    #region Public Methods
    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);

        _animator.SetTrigger(AnimatorID.Trigger.Hit);
    }
    #endregion
    #region Protected Methods
    protected override void _UpdateStates()
    {
        base._UpdateStates();

        hasTarget = target != null && target.isAlive;
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

        hasTarget = false;
        isRecognized = false;
        isAttackable = false;

        _bodyCollider.enabled = false;
        _animator.SetTrigger(AnimatorID.Trigger.Die);
    }
    #endregion
    #region Private Methods
    void _Move()
    {
        if (isRecognized)
        {
            if (_patrolCor != null)
            {
                StopCoroutine(_patrolCor);
                _patrolCor = null;
            }
            if (!isAttacking)
                _RushToTarget();
        }
        else
        {
            if (_patrolCor == null)
                _patrolCor = StartCoroutine(_Patrol(transform.position));
        }
    }
    void _RushToTarget()
    {
        _navMeshAgent.stoppingDistance = data.stoppingDistance;
        _navMeshAgent.destination = target.transform.position;

        if (isAttackable && _attackCor == null)
            _attackCor = StartCoroutine(_Attack());
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
                _navMeshAgent.destination = Algorithm.GetRandomPointOnNavMesh(transform.position, Random.Range(0f, data.patrolDistance));
            }
            else yield return null;
        }
    }
    IEnumerator _Attack()
    {
        // 플레이어 자리 쳐다보기
        isAttacking = true;
        _navMeshAgent.isStopped = true;
        yield return _rotate3D.StartCoroutine(_rotate3D.Rotate(target.transform.position, 0.5f));

        int idx = Random.Range(0, _attackClips.Length);
        _animator.SetTrigger(AnimatorID.Trigger.Attacks[idx]);
        yield return new WaitForSeconds(_attackClips[idx].length + 1f);

        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = target.transform.position;
        isAttacking = false;
        _attackCor = null;
    }
    #endregion

    #region Public Variables
    public bool hasTarget { get; private set; }
    public bool isRecognized { get; private set; } // 플레이어가 시야에 들어올 때
    public bool isAttackable { get; private set; } // 공격 가능할 때
    public bool isAttacking { get; private set; } // 공격 중일 때 true
    public Player target = null; // 몬스터가 따라갈 대상
    public MonsterData data = null; // 몬스터 데이터 컨테이너
    #endregion
    #region Protected Variables
    [SerializeField] protected AnimationClip[] _attackClips;
    protected Collider _bodyCollider = null;
    protected Animator _animator = null;
    protected NavMeshAgent _navMeshAgent = null;
    protected Rigidbody _rigidbody = null;
    protected Coroutine _patrolCor = null;
    protected Coroutine _attackCor = null;
    protected Rotate3D _rotate3D = null;
    #endregion
    #region Private Variables
    float _prevAttackTime = 0f;
    #endregion
}
