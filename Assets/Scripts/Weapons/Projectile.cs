using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private GameObject projectileVFX;

    private PlayerWeaponScriptableObjects _weaponScriptableObjects;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        var indestructible = other.GetComponent<Indestructible>();
        if (!other.isTrigger && (enemyHealth || indestructible))
        {
            if(enemyHealth)
                enemyHealth.TakeDamage(_weaponScriptableObjects);
            
            Instantiate(projectileVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, _startPosition) > _weaponScriptableObjects.WeaponRange)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateWeaponInfo(PlayerWeaponScriptableObjects playerWeaponScriptableObjects)
    {
        _weaponScriptableObjects = playerWeaponScriptableObjects;
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector2.right * flySpeed * Time.deltaTime);
    }
}
