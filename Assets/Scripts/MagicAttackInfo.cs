using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MagicAttackInfo
{
    public MagicAttack magicAttack;
    [Tooltip("효과 지연 시간")]
    public float effectDelayTime;
}
