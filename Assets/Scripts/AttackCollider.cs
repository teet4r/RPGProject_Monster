using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AttackCollider : MonoBehaviour
{
    void Awake()
    {
        _attackCollider = GetComponent<Collider>();

        if (_parent == null)
            _FindParent(transform);
    }
    void OnEnable()
    {
        _attackCollider.isTrigger = true;
    }
    /// <summary>
    /// MonsterObject�� ã�� �Լ�
    /// </summary>
    /// <param name="transform"></param>
    void _FindParent(Transform transform)
    {
        if (transform == null)
            return;
        if (transform.TryGetComponent(out MonsterObject monsterObject))
        {
            _parent = monsterObject;
            return;
        }
        _FindParent(transform.parent);
    }

    public AttackColliderData data = null;
    [SerializeField] MonsterObject _parent = null;
    Collider _attackCollider = null;
}
