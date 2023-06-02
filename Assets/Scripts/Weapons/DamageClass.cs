using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using Weapons;

public class DamageClass : MonoBehaviour
{

    private PlayerWeaponScriptableObjects _currentActiveWeapon;
    

    private void Start()
    {
        _currentActiveWeapon = (ActiveWeapon.Instance.CurrentActiveWeapon as IWeapon).GetWeaponInfo();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            enemyHealth.TakeDamage(_currentActiveWeapon);
        }
    }
}
