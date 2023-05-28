using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageClass : MonoBehaviour
{
    [SerializeField] private PlayerWeaponScriptableObjects swordWeaponScriptableObject;
    public delegate void EnemyWasHitWithDamage(PlayerWeaponScriptableObjects swordWeaponScriptableObject, EnemyHealth enemyHealth);

    public static event EnemyWasHitWithDamage OnEnemyWasHitWithDamage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            OnEnemyWasHitWithDamage?.Invoke(swordWeaponScriptableObject, enemyHealth);
        }
    }
}
