using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        SetCollision();
    }

    void SetCollision()
    {
        var psCollision = _particleSystem.collision;
        psCollision.enabled = true;
        psCollision.collidesWith = 0;
        for (int i = 0; i < layers.Length; i++)
            psCollision.collidesWith |= 1 << LayerMask.NameToLayer(layers[i]);
    }

    public float damage = 10f;
    protected ParticleSystem _particleSystem; // ÀÌÆåÆ® ÄÄÆ÷³ÍÆ®
    string[] layers =
    {
        Layer.Player,
        Layer.Ground
    };
}
