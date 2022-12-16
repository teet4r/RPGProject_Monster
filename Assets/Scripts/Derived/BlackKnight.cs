using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnight : MonsterObject
{
    protected override void _Die()
    {
        base._Die();

        //gameObject.SetActive(false);
    }
}
