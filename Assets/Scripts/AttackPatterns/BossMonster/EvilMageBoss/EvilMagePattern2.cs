using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMagePattern2 : MonoBehaviour, IAttackPattern
{
    public void Attack(Transform targetTransform)
    {
        StartCoroutine(_Attack(targetTransform));
    }

    IEnumerator _Attack(Transform targetTransform)
    {
        yield return _effectDelayTime;
        var targetPosition = targetTransform.position;
        targetPosition.y = 0f;
        for (int i = 0; i < _FlameCount; i++)
        {
            yield return _attackRate;
            Instantiate(_magicAttackPrefab, targetPosition, _magicAttackPrefab.transform.rotation);
        }
    }

    [SerializeField] GameObject _magicAttackPrefab;
    [SerializeField] int _FlameCount = 1;
    WaitForSeconds _effectDelayTime = new WaitForSeconds(0.5f);
    WaitForSeconds _attackRate = new WaitForSeconds(0.15f);
}
