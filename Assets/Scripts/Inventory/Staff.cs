using System;
using PlayerScripts;
using Unity.Mathematics;
using UnityEngine;
using Weapons;

namespace Inventory
{
    public class Staff : MonoBehaviour, IWeapon
    {
        [SerializeField] private PlayerWeaponScriptableObjects weaponInfo;
        [SerializeField] private GameObject magicLaser;
        [SerializeField] private Transform magicLaserSpawnPoint;

        private Animator _staffAnimator;
        private static readonly int AttackHash = Animator.StringToHash("Attack");

        private void Awake()
        {
            _staffAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            PlayerController.OnMouseMoveEventWithDirection += MouseFollowWithOffset;
        }

        private void OnDisable()
        {
            PlayerController.OnMouseMoveEventWithDirection -= MouseFollowWithOffset;
        }

        public void Attack()
        {
            _staffAnimator.SetTrigger(AttackHash);
        }

        public void SpawnStaffProjectileAnimationEvent()
        {
            var laser = Instantiate(magicLaser, magicLaserSpawnPoint.position, quaternion.identity);
            if (laser.TryGetComponent<MagicLaser>(out var mag))
            {
                mag.UpdateLaserRange(weaponInfo.WeaponRange);
            }
        }
        
        public PlayerWeaponScriptableObjects GetWeaponInfo()
        {
            return weaponInfo;
        }

        
        private void MouseFollowWithOffset(bool shouldFlip, float moveX, float moveY)
        {
            var angle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg;
            if (shouldFlip)
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            else
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}