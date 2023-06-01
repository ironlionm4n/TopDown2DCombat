using System;
using PlayerScripts;
using UnityEngine;
using Weapons;

namespace Inventory
{
    public class Staff : MonoBehaviour, IWeapon
    {
        [SerializeField] private PlayerWeaponScriptableObjects weaponInfo;
        
        private void OnEnable()
        {
            PlayerController.OnMouseMoveEventWithDirection += MouseFollowWithOffset;
        }

        public void Attack()
        {
            Debug.Log("Staff Attack");
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