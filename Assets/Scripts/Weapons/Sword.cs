using System;
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

        
        private Animator _swordAnimator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private ActiveWeapon _activeWeapon;
        private GameObject _slashGameObject;

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

        private void HandlePlayerAttack(InputAction.CallbackContext context, Transform playerTransform)
        {
            _swordAnimator.SetTrigger(Attack);
            weaponCollider.gameObject.SetActive(true);
            _slashGameObject = Instantiate(slashEffectPrefab, slashEffectSpawnLocation.position, Quaternion.identity);
            _slashGameObject.transform.SetParent(transform.parent);
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
            {
                _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            }
            else
            {
                _activeWeapon.transform.rotation = Quaternion.Euler(0, 0 ,angle);
            }
        }

        public void SwingUpFlipAnimationEvent()
        {
            _slashGameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        }
    }
}