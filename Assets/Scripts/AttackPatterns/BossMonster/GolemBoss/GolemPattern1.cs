using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPattern1 : MonoBehaviour, IAttackPattern
{
    public void Attack(Vector3 targetPosition)
    {
        StartCoroutine(_Attack(targetPosition));
    }

    IEnumerator _Attack(Vector3 targetPosition)
    {
        yield return _effectDelayTime;
        for (int i = 0; i < _meteorCount; i++)
        {
            var newTargetPos = new Vector3(targetPosition.x + Random.Range(-5f, 5f), 0f, targetPosition.z + Random.Range(-5f, 5f));
            Instantiate(_magicAttackPrefab, newTargetPos, Quaternion.identity);
            yield return _attackRate;
        }
    }

    [SerializeField] GameObject _magicAttackPrefab;
    [SerializeField] int _meteorCount = 12;
    WaitForSeconds _effectDelayTime = new WaitForSeconds(0.5f);
    WaitForSeconds _attackRate = new WaitForSeconds(0.15f);
}
