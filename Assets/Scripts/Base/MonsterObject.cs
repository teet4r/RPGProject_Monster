using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// LifeObject를 상속하는 하위 클래스
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class MonsterObject : LifeObject
{
    protected virtual void Awake()
    {
        _bodyCollider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator= GetComponent<Animator>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        hasTarget = false;
        isRecognized = false;
        isAttackable = false;

        _bodyCollider.isTrigger = true;
        _bodyCollider.enabled = true;

        _prevAttackTime = 0f;

        _navMeshAgent.isStopped = false;
        _navMeshAgent.stoppingDistance = data.stoppingDistance;
        _navMeshAgent.speed = data.moveSpeed;
        _navMeshAgent.angularSpeed = 360f;
        _navMeshAgent.acceleration = 50f;
    }
    protected override void Update()
    {
        base.Update();

        _Move();

        _animator.SetBool(AnimatorID.Bool.IsWalking, isWalking);
        _animator.SetInteger(AnimatorID.Int.Attack, isAttackable ? Random.Range(0, data.attackMotionCount) : -1);
    }
    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);

        _animator.SetTrigger(AnimatorID.Trigger.Hit);
    }
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

        _patrolCor = null;
        _bodyCollider.enabled = false;
        _animator.SetTrigger(AnimatorID.Trigger.Die);
    }
    void _LookAt(Vector3 target)
    {
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
    }
    void _Move()
    {
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

    public bool hasTarget { get; private set; }
    public bool isRecognized { get; private set; } // 플레이어가 시야에 들어올 때
    public bool isAttackable { get; private set; }
    public Player target = null; // 몬스터가 따라갈 대상
    public MonsterData data = null; // 몬스터 데이터 컨테이너
    protected Collider _bodyCollider = null;
    protected Animator _animator = null;
    protected NavMeshAgent _navMeshAgent = null;
    protected Rigidbody _rigidbody = null;
    protected Coroutine _patrolCor = null;
    [SerializeField] protected AttackCollider[] attackColliders = null;
    float _prevAttackTime = 0f;
}
