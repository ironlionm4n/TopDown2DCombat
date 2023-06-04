using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private GameObject projectileVFX;
    [SerializeField] private float projectileRange = 10f;
    [SerializeField] private bool isEnemyProjectile;
    public bool IsEnemyProjectile => isEnemyProjectile;
    
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
        var playerHealth = other.GetComponent<PlayerHealth>();
        if (!other.isTrigger && (enemyHealth || indestructible || playerHealth))
        {
            if ((playerHealth && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                playerHealth?.TakeDamage(1, transform);
                Instantiate(projectileVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if(!other.isTrigger && indestructible)
            {
                Instantiate(projectileVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, _startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        flySpeed = moveSpeed;
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector2.right * flySpeed * Time.deltaTime);
    }
}
