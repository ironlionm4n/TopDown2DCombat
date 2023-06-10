using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private bool spawnsPickups;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spawnsPickups)
            if (other.GetComponent<DamageClass>() || other.GetComponent<Projectile>())
            {
                GetComponent<PickupSpawner>().SpawnPickup();
                Instantiate(destroyVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
    }
}