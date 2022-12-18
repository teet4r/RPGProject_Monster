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

        _isInvincible = false;
    }
    protected override void Update()
    {
        base.Update();

        Debug.Log(curHp);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AttackCollider attackCollider))
            StartCoroutine(_TriggerGetDamage(attackCollider.parent.data.damage));
    }
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out AttackCollider attackCollider))
            StartCoroutine(_TriggerGetDamage(attackCollider.parent.data.damage));
    }
    protected override void _Die()
    {
        base._Die();

        gameObject.SetActive(false);
    }
    IEnumerator _TriggerGetDamage(float damage)
    {
        if (_isInvincible) yield break;
        _isInvincible = true;
        GetDamage(damage);
        yield return new WaitForSeconds(_invincibleTime);
        _isInvincible = false;
    }

    [SerializeField] float _invincibleTime = 1f;
    MovementController _movementController = null;
    bool _isInvincible;
}
