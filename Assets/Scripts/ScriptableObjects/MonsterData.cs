using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObject/MonsterData")]
public class MonsterData : ScriptableObject
{
    public float moveSpeed = 7f;
    [Tooltip("body damage")]
    public float damage = 10f;
    public float stoppingDistance = 3f;
    public float patrolDistance = 30f;
    public float recognitionDistance = 20f;
    public float attackRate = 1f;
}
