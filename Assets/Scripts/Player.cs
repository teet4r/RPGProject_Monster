using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void OnEnable()
    {
        isAlive = true;
    }

    public bool isAlive;
}
