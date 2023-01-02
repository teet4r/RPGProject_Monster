using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPattern2 : MonoBehaviour, IAttackPattern
{
    public void Attack(Transform targetTransform)
    {
        StartCoroutine(_Attack(targetTransform));
    }

    IEnumerator _Attack(Transform targetTransform)
    {
        yield return _effectDelayTime;
        var newTargetPosition = new Vector3(
            targetTransform.position.x,
            targetTransform.position.y + 15f,
            targetTransform.position.z
        );
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
