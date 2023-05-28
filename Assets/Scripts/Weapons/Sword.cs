using System;
using System.Collections;
using UnityEngine;
using PlayerScripts;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private GameObject slashEffectPrefab;
        [SerializeField] private Transform slashEffectSpawnLocation;
        [SerializeField] private Transform weaponCollider;
        [SerializeField] private float swordAttackCooldown;

        private Animator _swordAnimator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private ActiveWeapon _activeWeapon;
        private GameObject _slashGameObject;
        private bool _attackButtonDown, _isAttacking;

        private void Awake()
        {
            _swordAnimator = GetComponent<Animator>();
            _activeWeapon = GetComponentInParent<ActiveWeapon>();
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerAttackEvent += HandlePlayerAttack;
            PlayerController.OnPlayerAttackCancelledEvent += HandlePlayerAttackCancelled;
            PlayerController.OnMouseMoveEventWithDirection += MouseFollowWithOffset;
            
        }

        private void Update()
        {
            if (_attackButtonDown && !_isAttacking) SwordAttack();
        }

        private void HandlePlayerAttackCancelled(InputAction.CallbackContext context)
        {
            if (context.canceled) _attackButtonDown = false;
        }

        private void HandlePlayerAttack(InputAction.CallbackContext context, Transform playerTransform)
        {
            _attackButtonDown = true;
        }

        private void SwordAttack()
        {
            _isAttacking = true;
            _swordAnimator.SetTrigger((int)Attack);
            weaponCollider.gameObject.SetActive(true);
            _slashGameObject = Instantiate(slashEffectPrefab, slashEffectSpawnLocation.position, Quaternion.identity);
            _slashGameObject.transform.SetParent(transform.parent);
        }

        public void SetIsAttackingFalseAnimationEvent()
        {
            StartCoroutine(SwordAttackCDRoutine());
        }

        private IEnumerator SwordAttackCDRoutine()
        {
            yield return new WaitForSeconds(swordAttackCooldown);
            _isAttacking = false;
        }

        public void DisableWeaponColliderAnimationEvent()
        {
            weaponCollider.gameObject.SetActive(false);
        }

        private void MouseFollowWithOffset(bool shouldFlip, float moveX, float moveY)
        {
            var angle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg;
            slashEffectPrefab.GetComponent<SpriteRenderer>().flipX = shouldFlip;
            weaponCollider.rotation = Quaternion.Euler(0, shouldFlip ? 180 : 0, 0);
            if (shouldFlip)
                _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            else
                _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SwingUpFlipAnimationEvent()
        {
            _slashGameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        }
    }
}