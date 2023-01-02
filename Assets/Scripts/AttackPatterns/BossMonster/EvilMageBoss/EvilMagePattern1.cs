using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMagePattern1 : MonoBehaviour, IAttackPattern
{
    public void Attack(Transform targetTransform)
    {
        StartCoroutine(_Attack(targetTransform));
    }

    IEnumerator _Attack(Transform targetTransform)
    {
        yield return _effectDelayTime;
        for (int i = 0; i < _explosionCount; i++)
        {
            var newTargetPos = new Vector3(
                targetTransform.position.x,
                0f,
                targetTransform.position.z
            );
            var newMyEuler = new Vector3(_magicAttackPrefab.transform.eulerAngles.x, Random.Range(0f, 360f), _magicAttackPrefab.transform.eulerAngles.z);
            yield return _attackRate;
            var clone = Instantiate(_magicAttackPrefab, newTargetPos, _magicAttackPrefab.transform.rotation);
            clone.transform.eulerAngles = newMyEuler;
        }
    }

    [SerializeField] GameObject _magicAttackPrefab;
    [SerializeField] int _explosionCount = 5;
    WaitForSeconds _effectDelayTime = new WaitForSeconds(0.5f);
    WaitForSeconds _attackRate = new WaitForSeconds(0.3f);
}
