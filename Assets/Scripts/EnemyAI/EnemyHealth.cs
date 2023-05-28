using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyScriptableObject;
    [SerializeField] private float hitDelayMovementTime;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float hitFlashTime;

    public delegate void EnemyDeathHandler(EnemyHealth enemyHealth);

    public static event EnemyDeathHandler OnEnemyDeath;

    private int _currentHealth;
    private ApplyKnockback _knockback;
    private EnemyPathfinding _enemyPathfinding;
    private Transform _playerTransform;
    private SpriteRenderer _enemySpriteRenderer;
    private Material _defaultMaterial;

    private void Awake()
    {
        _knockback = GetComponent<ApplyKnockback>();
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _enemySpriteRenderer.material;
    }

    private void Start()
    {
        _currentHealth = enemyScriptableObject.StartingHealth;
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerAttackEvent += HandleOnPlayerAttack;
        DamageClass.OnEnemyWasHitWithDamage += HandleTakeDamage;
    }

    private void HandleTakeDamage(PlayerWeaponScriptableObjects weaponSO)
    {
        TakeDamage(weaponSO);
    }

    private void HandleOnPlayerAttack(InputAction.CallbackContext context, Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void TakeDamage(PlayerWeaponScriptableObjects weaponSO)
    {
        _enemyPathfinding.SetShouldMove(false);
        _currentHealth -= weaponSO.WeaponDamage;
        StartCoroutine(FlashDamageSpriteColor());
        _knockback.GetKnockedBack(_playerTransform, weaponSO.KnockBackForce);
        
        if (_currentHealth > 0)
        {
            StartCoroutine(RegainMovement());
        }
    }

    private IEnumerator FlashDamageSpriteColor()
    {
        _enemySpriteRenderer.material = damageMaterial;
        yield return new WaitForSeconds(hitFlashTime);
        if (_currentHealth <= 0)
        {
            OnEnemyDeath?.Invoke(this);
            DestroySelf();
        }
        else
        {
            _enemySpriteRenderer.material = _defaultMaterial;
        }
    }

    private IEnumerator RegainMovement()
    {
        yield return new WaitForSeconds(hitDelayMovementTime);
        _enemyPathfinding.SetShouldMove(true);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}