using System;
using System.Collections;
using UnityEngine;
using Weapons;
using MISC;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    private const string HEALTH_SLIDER = "Health Slider";
    
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount;
    [SerializeField] private float invulnerabilityTime;

    private Slider _healthSlider;
    private int _currentHealth;
    private ApplyKnockback _knockback;
    private Flash _flash;
    private bool _canTakeDamage;

    protected override void Awake()
    {
        base.Awake();
        _knockback = GetComponent<ApplyKnockback>();
        _flash = GetComponent<Flash>();
        _canTakeDamage = true;

    }

    private void Start()
    {
        _currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        var enemyAi = other.gameObject.GetComponent<EnemyAI>();

        if (enemyAi) TakeDamage(1, other.transform);
    }

    public void HealPlayer()
    {
        if (_currentHealth < maxHealth)
        {
            _currentHealth += 1;
            UpdateHealthSlider();
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
        UpdateHealthSlider();
        StartCoroutine(ResetCanTakeDamageRoutine());
        CheckForPlayerDeath();
    }

    private IEnumerator ResetCanTakeDamageRoutine()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        _canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (_healthSlider == null)
        {
            _healthSlider = GameObject.Find(HEALTH_SLIDER).GetComponent<Slider>();
        }
        
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = _currentHealth;
    }

    private void CheckForPlayerDeath()
    {
        if (_currentHealth <= 0) _currentHealth = 0;
    }
}