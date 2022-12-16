using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LifeObject
{
    void Awake()
    {
        _movementController = GetComponent<MovementController>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        _prevHitTime = 0f;
        _isInvincible = false;
    }
    protected override void Update()
    {
        base.Update();

        Debug.Log(curHp);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!_isInvincible && other.TryGetComponent(out AttackCollider attackCollider))
        {
            GetDamage(attackCollider.damage);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out AttackCollider attackCollider))
        {
            GetDamage(attackCollider.damage);
        }
    }

    IEnumerator _TriggerInvincible()
    {
        if (_isInvincible) yield break;
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibleTime);
        _isInvincible = false;
    }

    [SerializeField] float _invincibleTime = 1f;
    MovementController _movementController = null;
    bool _isInvincible;
    float _prevHitTime; // 이전 피격 시간
}
