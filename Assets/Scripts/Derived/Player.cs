using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LifeObject
{
    #region Unity Messages
    protected override void Awake()
    {
        base.Awake();

        _movementController = GetComponent<MovementController>();
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
    void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out MagicAttack magicAttack))
            GetDamage(magicAttack.damage);
    }
    #endregion

    protected override void _Die()
    {
        base._Die();

        gameObject.SetActive(false);
    }
    
    MovementController _movementController = null;
}
