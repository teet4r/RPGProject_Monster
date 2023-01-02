using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPattern1 : MonoBehaviour, IAttackPattern
{
    public void Attack(Transform targetTransform)
    {
        StartCoroutine(_Attack(targetTransform));
    }

    IEnumerator _Attack(Transform targetTransform)
    {
        yield return _effectDelayTime;
        for (int i = 0; i < _meteorCount; i++)
        {
            var newTargetPos = new Vector3(targetTransform.position.x, 0f, targetTransform.position.z);
            Instantiate(_magicAttackPrefab, newTargetPos, Quaternion.identity);
            yield return _attackRate;
        }
    }

    [SerializeField] GameObject _magicAttackPrefab;
    [SerializeField] int _meteorCount = 12;
    WaitForSeconds _effectDelayTime = new WaitForSeconds(0.5f);
    WaitForSeconds _attackRate = new WaitForSeconds(0.3f);
}
