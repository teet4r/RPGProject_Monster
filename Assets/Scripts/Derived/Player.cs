using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LifeObject
{
    #region Unity Messages
    void Awake()
    {
        _movementController = GetComponent<MovementController>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        _isInvincible = false;
    }
    void Start()
    {
        _wfs_invincible = new WaitForSeconds(_invincibleTime);
    }
    protected override void Update()
    {
        base.Update();

        Debug.Log(curHp);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AttackCollider attackCollider))
            GetDamage(attackCollider.parent.data.damage);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out AttackCollider attackCollider))
            GetDamage(attackCollider.parent.data.damage);
    }
    #endregion
    public override void GetDamage(float damageAmount)
    {
        if (!isAlive) return;
        if (_isInvincible) return;

        StartCoroutine(_TriggerGetDamage(damageAmount));
    }

    protected override void _Die()
    {
        base._Die();

        _isInvincible = false;

        gameObject.SetActive(false);
    }
    
    IEnumerator _TriggerGetDamage(float damage)
    {
        _isInvincible = true;

        curHp -= damage;
        if (curHp <= 0f)
            _Die();

        yield return _wfs_invincible;
        _isInvincible = false;
    }

    [SerializeField] float _invincibleTime = 1f;
    MovementController _movementController = null;
    WaitForSeconds _wfs_invincible;
    bool _isInvincible;
}
