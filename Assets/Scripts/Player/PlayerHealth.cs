using System;
using System.Collections;
using UnityEngine;
using Weapons;
using MISC;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount;
    [SerializeField] private float invulnerabilityTime;

    private int _currentHealth;
    private ApplyKnockback _knockback;
    private Flash _flash;
    private bool _canTakeDamage;

    private void Awake()
    {
        _knockback = GetComponent<ApplyKnockback>();
        _flash = GetComponent<Flash>();
        _canTakeDamage = true;
    }

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        
        var enemyAi = other.gameObject.GetComponent<EnemyAI>();
        
        if (enemyAi)
        {
            TakeDamage(1, other.transform);
        }
    }

    public void TakeDamage(int damage, Transform hitTransform)
    {
        if (!_canTakeDamage) return;
        
        ScreenShakeManager.Instance.ShakeScreen();
        _knockback.GetKnockedBack(hitTransform.transform, knockBackThrustAmount);
        StartCoroutine(_flash.FlashRoutine());
        _canTakeDamage = false;
        _currentHealth -= damage;
        StartCoroutine(ResetCanTakeDamageRoutine());
    }

    private IEnumerator ResetCanTakeDamageRoutine()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        _canTakeDamage = true;
    }
}