using System;
using System.Collections;
using Inventory;
using MISC;
using PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class ActiveWeapon : Singleton<ActiveWeapon>
    {
        public MonoBehaviour CurrentActiveWeapon
        {
            get;
            private set;
        }

        private bool _attackButtonDown, _isAttacking;
        private float _coolDownTime;

        private void Start()
        {
            StartCoroutine(TimeBetweenAttacks());
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerAttackEvent += HandlePlayerAttack;
            PlayerController.OnPlayerAttackCancelledEvent += HandlePlayerAttackCancelled;
        }

        private void Update()
        {
            if (_attackButtonDown && !_isAttacking) Attack();
        }

        private void OnDisable()
        {
            PlayerController.OnPlayerAttackEvent -= HandlePlayerAttack;
            PlayerController.OnPlayerAttackCancelledEvent -= HandlePlayerAttackCancelled;
        }

        private IEnumerator TimeBetweenAttacks()
        {
            yield return new WaitForSeconds(_coolDownTime);
            _isAttacking = false;
        }

        private void HandlePlayerAttackCancelled(InputAction.CallbackContext context)
        {
            if (context.canceled) _attackButtonDown = false;
        }

        private void HandlePlayerAttack(InputAction.CallbackContext context, Transform playerTransform)
        {
            _attackButtonDown = true;
        }

        public void NewWeapon(MonoBehaviour newWeapon)
        {
            CurrentActiveWeapon = newWeapon;
            _coolDownTime = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().WeaponCooldown;
            StartCoroutine(TimeBetweenAttacks());
        }

        public void WeaponNull()
        {
            CurrentActiveWeapon = null;
        }

        private void Attack()
        {
            if (_attackButtonDown && !_isAttacking)
            {
                _isAttacking = true;
                (CurrentActiveWeapon as IWeapon)?.Attack();
                StopAllCoroutines();
                StartCoroutine(TimeBetweenAttacks());
            }
        }
    }
}