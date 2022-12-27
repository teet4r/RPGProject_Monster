using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ParticleSystem))]
public class MagicAttack : MonoBehaviour
{
    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    void OnEnable()
    {
        _particleSystem.Play();     // ����Ʈ ���
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out Player player))
            player.GetDamage(damage);
    }

    public float damage = 10f;
    protected ParticleSystem _particleSystem; // ����Ʈ ������Ʈ
}
