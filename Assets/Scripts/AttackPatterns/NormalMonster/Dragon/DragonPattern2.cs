using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPattern2 : MonoBehaviour, IAttackPattern
{
    public void Attack(Vector3 targetPosition)
    {
        StartCoroutine(_Attack(targetPosition));
    }

    IEnumerator _Attack(Vector3 targetPosition)
    {
        yield return _effectDelayTime;
        for (int i = 0; i < _fireBreathCount; i++)
        {
            var clone = Instantiate(_magicAttackPrefab, transform);
            yield return _attackRate;
        }
    }

    [SerializeField] GameObject _magicAttackPrefab;
    [SerializeField] int _fireBreathCount = 1;
    WaitForSeconds _effectDelayTime = new WaitForSeconds(0.75f);
    WaitForSeconds _attackRate = new WaitForSeconds(0.15f);
}
