using System;
using UnityEngine;
using Weapons;

namespace Inventory
{
    public class Bow : MonoBehaviour, IWeapon
    {
        [SerializeField] private PlayerWeaponScriptableObjects weaponInfo;
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform arrowSpawnPoint;

        private Animator _bowAnimator;
        private static readonly int LaunchHash = Animator.StringToHash("Launch");


        private void Awake()
        {
            _bowAnimator = GetComponent<Animator>();
        }

        public void Attack()
        {
            _bowAnimator.SetTrigger(LaunchHash);
            var arrowSpawn = Instantiate(arrowPrefab, arrowSpawnPoint.position,
                ActiveWeapon.Instance.transform.rotation);
            arrowSpawn.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
        }

        public PlayerWeaponScriptableObjects GetWeaponInfo()
        {
            return weaponInfo;
        }
    }
}