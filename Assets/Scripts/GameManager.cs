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
    }

    IEnumerator _FindPlayer()
    {
        WaitForSeconds wfs = new WaitForSeconds(_findPlayerRate);
        while (true)
        {
            // FindObjectOfType<T>:
            // 하나의 플레이어만 찾음.
            // 플레이어가 여럿이라면 그 중 첫번째로 발견한 플레이어를 리턴.
            player = FindObjectOfType<Player>();
            yield return wfs;
        }
    }

    public static GameManager instance = null;
    public Player player { get; private set; } = null;
    [Tooltip("플레이어를 주기적으로 찾는 시간")]
    [SerializeField]
    float _findPlayerRate = 3f;
}
