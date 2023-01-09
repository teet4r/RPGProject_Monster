using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        StartCoroutine(_FindPlayer());
        StartCoroutine(_MakeMonsters());
    }

    IEnumerator _MakeMonsters()
    {
        while (true)
        {
            var objs = ObjectPool.instance.GetRandoms(2);
            for (int i = 0; i < objs.Count; i++)
            {
                objs[i].transform.position = Vector3.zero;
                objs[i].SetActive(true);
                yield return new WaitForSeconds(1f);
            }
        }
    }

    IEnumerator _FindPlayer()
    {
        WaitForSeconds wfs = new WaitForSeconds(_findPlayerRate);
        while (true)
        {
            // FindObjectOfType<T>:
            // �ϳ��� �÷��̾ ã��.
            // �÷��̾ �����̶�� �� �� ù��°�� �߰��� �÷��̾ ����.
            player = FindObjectOfType<Player>();
            yield return wfs;
        }
    }

    public static GameManager instance = null;
    public Player player { get; private set; } = null;
    [Tooltip("�÷��̾ �ֱ������� ã�� �ð�")]
    [SerializeField]
    float _findPlayerRate = 3f;
}
