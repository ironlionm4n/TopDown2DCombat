using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float shootingCooldown;

    private bool _isShooting;

    public void Attack()
    {
        if (!_isShooting) StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        _isShooting = true;
        for (var i = 0; i < burstCount; i++)
        {
            var targetDirection = PlayerController.Instance.transform.position - transform.position;

            var spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            spawnedBullet.transform.right = targetDirection;

            if (spawnedBullet.TryGetComponent<Projectile>(out var projectile))
                projectile.UpdateMoveSpeed(bulletMoveSpeed);
            
            yield return new WaitForSeconds(timeBetweenBursts);
        }

        yield return new WaitForSeconds(shootingCooldown);
        _isShooting = false;
    }
}