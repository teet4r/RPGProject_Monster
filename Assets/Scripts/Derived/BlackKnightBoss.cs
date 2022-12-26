using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightBoss : BossMonsterObject
{
    protected override void _Die()
    {
        base._Die();

        //gameObject.SetActive(false);
    }
}
