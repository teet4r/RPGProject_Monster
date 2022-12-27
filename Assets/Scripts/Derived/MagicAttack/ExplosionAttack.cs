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
        StartCoroutine(_Attack());  // 실제 공격 실행
        _particleSystem.Play();     // 폭발 이펙트 재생
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

    [Tooltip("충돌 유효 시간; 이 시간 동안에는 플레이어가 피격 데미지를 입을 수 있음.")]
    [SerializeField] float _hitValidTime = 0.25f;
    [SerializeField] float _hitRadius = 1;
    float _prevTime;
}
