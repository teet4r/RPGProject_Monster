using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMage : MonsterObject
{
    protected override void _Die()
    {
        base._Die();

        //gameObject.SetActive(false);
    }
    protected override IEnumerator _Attack()
    {
        yield return null;
    }
}
