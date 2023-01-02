using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPattern2 : MonoBehaviour, IAttackPattern
{
    public void Attack(Transform targetTransform)
    {
        StartCoroutine(_Attack(targetTransform));
    }

    IEnumerator _Attack(Transform targetTransform)
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
