using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviour를 상속하는 최상위 클래스
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

        isWalking = transform.position - _prevPos == Vector3.zero ? false : true;
        _prevPos = transform.position;
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
            Die();
    }

    protected virtual void Die()
    {
        isAlive = false;
        isWalking = false;
        curHp = 0;
    }

    public bool isAlive { get; protected set; }
    public bool isWalking { get; private set; }
    public float maxHp { get { return _maxHp; } }
    public float curHp { get; protected set; }

    [SerializeField]
    protected float _maxHp = 50f;

    Vector3 _prevPos;
}
