using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();

        if (!_fallDown)
            _rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        _Move();
    }

    void _Move()
    {
        var moveDistance =
            (Input.GetAxis(_strVertical) * Vector3.forward +
            Input.GetAxis(_strHorizontal) * Vector3.right).normalized *
            moveSpeed *
            Time.deltaTime;
        _rigid.MovePosition(_rigid.position + moveDistance); // ���� ��ġ�� �̵������ִ� �Լ�
    }

    public float moveSpeed { get { return _moveSpeed; } }

    [SerializeField]
    float _moveSpeed = 10f;
    [SerializeField]
    bool _fallDown = false;

    Rigidbody _rigid;

    readonly string _strVertical = "Vertical";
    readonly string _strHorizontal = "Horizontal";
}
