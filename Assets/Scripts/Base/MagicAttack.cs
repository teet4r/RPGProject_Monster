using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public abstract class MagicAttack : MonoBehaviour
{
    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public abstract void Attack(Vector3 position);

    public float damage { get { return _damage; } }
    protected ParticleSystem _particleSystem; // ����Ʈ ������Ʈ
    [SerializeField] protected float _damage = 10f;
}
