using System;
using UnityEngine;
using PlayerScripts;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class Sword : MonoBehaviour
    {
        private Animator _swordAnimator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private ActiveWeapon _activeWeapon;

        private void Awake()
        {
            _swordAnimator = GetComponent<Animator>();
            _activeWeapon = GetComponentInParent<ActiveWeapon>();
            
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerAttackEvent += HandlePlayerAttack;
            PlayerController.OnMouseMoveEventWithDirection += MouseFollowWithOffset;
        }

        private void HandlePlayerAttack(InputAction.CallbackContext context)
        {
            _swordAnimator.SetTrigger(Attack);
        }

        private void MouseFollowWithOffset(bool shouldFlip, float moveX, float moveY)
        {
            var angle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg;
            if (shouldFlip)
            {
                _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            }
            else
            {
                _activeWeapon.transform.rotation = Quaternion.Euler(0, 0 ,angle);
            }
        }
    }
}