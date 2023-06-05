using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    private SpriteFade _spriteFade;

    private void Awake()
    {
        _spriteFade = GetComponent<SpriteFade>();
    }

    private void Start()
    {
        StartCoroutine(_spriteFade.SlowFadeRoutine());
        
        Invoke(nameof(DisableCollider), 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            playerHealth.TakeDamage(1, transform);
        }
    }

    private void DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
