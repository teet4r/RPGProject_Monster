using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAssassin : MonsterObject
{
    protected override void _Die()
    {
        base._Die();

        //gameObject.SetActive(false);
    }
}