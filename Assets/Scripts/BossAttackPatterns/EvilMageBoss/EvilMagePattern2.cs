using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMagePattern2 : MonoBehaviour, IAttackPattern
{
    public void Attack(Vector3 targetPosition)
    {
        StartCoroutine(_Attack(targetPosition));
    }

    IEnumerator _Attack(Vector3 targetPosition)
    {
        yield return _effectDelayTime;
        targetPosition.y = 0f;
        for (int i = 0; i < _FlameCount; i++)
        {
            Instantiate(_magicAttackPrefab, targetPosition, _magicAttackPrefab.transform.rotation);
            yield return _attackRate;
        }
    }

    [SerializeField] GameObject _magicAttackPrefab;
    [SerializeField] int _FlameCount = 1;
    WaitForSeconds _effectDelayTime = new WaitForSeconds(0.5f);
    WaitForSeconds _attackRate = new WaitForSeconds(0.15f);
}
