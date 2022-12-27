using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MagicAttack
{
    void OnEnable()
    {
        _damage = 55f;
    }
    public override void Attack(Vector3 position)
    {
        transform.position = position;
        _prevTime = Time.time;
        StartCoroutine(_Attack());  // ���� ���� ����
        _particleSystem.Play();     // ���� ����Ʈ ���
    }

    IEnumerator _Attack()
    {
        while (Time.time - _prevTime <= _hitValidTime)
        {
            var hitColliders = Physics.OverlapSphere(transform.position, _hitRadius, 1 << 6);
            for (int i = 0; i < hitColliders.Length; i++)
                if (hitColliders[i].TryGetComponent(out Player player) && player.isAlive)
                {
                    player.GetDamage(_damage);
                    break;
                }
            yield return null;
        }
    }

    [Tooltip("�浹 ��ȿ �ð�; �� �ð� ���ȿ��� �÷��̾ �ǰ� �������� ���� �� ����.")]
    [SerializeField] float _hitValidTime = 0.25f;
    [SerializeField] float _hitRadius = 1;
    float _prevTime;
}