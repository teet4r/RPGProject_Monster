using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPattern2 : MonoBehaviour, IAttackPattern
{
    public void Attack(Vector3 targetPosition)
    {
        StartCoroutine(_Attack(targetPosition));
    }

    IEnumerator _Attack(Vector3 targetPosition)
    {
        yield return _effectDelayTime;
        var newTargetPosition = new Vector3(targetPosition.x, targetPosition.y + 20f, targetPosition.z);
        for (int i = 0; i < _knivesCount; i++)
        {
            Instantiate(_magicAttackPrefab, newTargetPosition, _magicAttackPrefab.transform.rotation);
            yield return _attackRate;
        }
    }

    [SerializeField] GameObject _magicAttackPrefab;
    [SerializeField] int _knivesCount = 3;
    WaitForSeconds _effectDelayTime = new WaitForSeconds(0.5f);
    WaitForSeconds _attackRate = new WaitForSeconds(0.3f);
}
