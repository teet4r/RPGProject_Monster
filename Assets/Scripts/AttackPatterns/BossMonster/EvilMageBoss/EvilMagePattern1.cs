using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMagePattern1 : MonoBehaviour, IAttackPattern
{
    public void Attack(Vector3 targetPosition)
    {
        StartCoroutine(_Attack(targetPosition));
    }

    IEnumerator _Attack(Vector3 targetPosition)
    {
        yield return _effectDelayTime;
        for (int i = 0; i < _explosionCount; i++)
        {
            var newTargetPos = new Vector3(targetPosition.x + Random.Range(-3.5f, 3.5f), 0f, targetPosition.z + Random.Range(-3.5f, 3.5f));
            var newMyEuler = new Vector3(_magicAttackPrefab.transform.eulerAngles.x, Random.Range(0f, 360f), _magicAttackPrefab.transform.eulerAngles.z);
            var clone = Instantiate(_magicAttackPrefab, newTargetPos, _magicAttackPrefab.transform.rotation);
            clone.transform.eulerAngles = newMyEuler;
            yield return _attackRate;
        }
    }

    [SerializeField] GameObject _magicAttackPrefab;
    [SerializeField] int _explosionCount = 5;
    WaitForSeconds _effectDelayTime = new WaitForSeconds(0.5f);
    WaitForSeconds _attackRate = new WaitForSeconds(0.15f);
}
