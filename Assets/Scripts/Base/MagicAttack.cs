using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ParticleSystem))]
public class MagicAttack : MonoBehaviour
{
    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    void OnEnable()
    {
        _particleSystem.Play();     // ÀÌÆåÆ® Àç»ý

        var psCollision = _particleSystem.collision;
        psCollision.collidesWith = 0;
        for (int i = 0; i < collidesWithLayers.Length; i++)
            psCollision.collidesWith |= LayerMask.NameToLayer(collidesWithLayers[i]);
    }

    public float damage = 10f;
    protected ParticleSystem _particleSystem; // ÀÌÆåÆ® ÄÄÆ÷³ÍÆ®
    string[] collidesWithLayers =
    {
        "Player",
        "Ground"
    };
}
