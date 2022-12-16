using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviour를 상속하는 최상위 클래스이며
/// 생명체에 기본 적용되는 클래스
/// </summary>
public class LifeObject : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        isAlive = true;
        isWalking = false;
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

        curHp -= damageAmount;
        if (curHp <= 0f)
            _Die();
    }
    protected virtual void _Die()
    {
        if (!isAlive) return;

        isAlive = false;
        isWalking = false;
        curHp = 0;
    }
    protected virtual void _UpdateStates()
    {
        isWalking = transform.position - _prevPos == Vector3.zero ? false : true;
        _prevPos = transform.position;
    }

    public bool isAlive { get; private set; }
    public bool isWalking { get; private set; }
    public float maxHp { get { return _maxHp; } }
    public float curHp { get; protected set; }
    [SerializeField] protected float _maxHp = 50f;
    Vector3 _prevPos;
}
