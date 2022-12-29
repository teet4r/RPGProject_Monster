using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : NormalMonsterObject
{
    protected override void _Die()
    {
        base._Die();

        //gameObject.SetActive(false);
    }
}
