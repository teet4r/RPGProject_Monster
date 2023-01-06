using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviour를 상속하는 최상위 클래스이며
/// 생명체에 기본 적용되는 클래스
/// </summary>
public class LifeObject : MonoBehaviour
{
    protected virtual void Awake()
    {
        _wfs_invincible = new WaitForSeconds(_invincibleTime);
    }
    protected virtual void OnEnable()
    {
        isAlive = true;
        isWalking = false;
        isInvincible = false;
        curHp = maxHp;

        _prevPos = transform.position;
    }
    protected virtual void Update()
    {
        if (!isAlive) return;

        _UpdateStates();
    }

    public virtual void Heal(float healAmount)
    {
        if (!isAlive) return;

        curHp = Mathf.Min(curHp + healAmount, maxHp);
    }
    public virtual void GetDamage(float damageAmount)
    {
        if (!isAlive) return;
        if (isInvincible) return;

        StartCoroutine(_TriggerGetDamage(damageAmount));
    }
    protected virtual void _Die()
    {
        if (!isAlive) return;

        isAlive = false;
        isWalking = false;
        isInvincible = false;

        curHp = 0f;
    }
    protected virtual void _UpdateStates()
    {
        isWalking = transform.position - _prevPos == Vector3.zero ? false : true;
        _prevPos = transform.position;
    }
    IEnumerator _TriggerGetDamage(float damage)
    {
        isInvincible = true;

        curHp -= damage;
        if (curHp <= 0f)
            _Die();

        yield return _wfs_invincible;
        isInvincible = false;
    }

    public bool isAlive { get; private set; }
    public bool isWalking { get; private set; }
    public bool isInvincible { get; protected set; }
    public float maxHp { get { return _maxHp; } }
    public float curHp { get; protected set; }
    [SerializeField] protected float _maxHp = 50f;
    [Tooltip("피격 후 무적 시간")]
    [SerializeField] float _invincibleTime = 0.5f;
    WaitForSeconds _wfs_invincible = null;
    Vector3 _prevPos;
}
