using System;
using System.Collections;
using UnityEngine;
using PlayerScripts;
using UnityEngine.InputSystem;
using Weapons;

namespace Inventory
{
    public class Sword : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject slashEffectPrefab;
        [SerializeField] private PlayerWeaponScriptableObjects weaponInfo;
        
        private Animator _swordAnimator;
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private GameObject _slashGameObject;
        private Transform _weaponColliderTransform;

        private void Awake()
        {
            _swordAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            _weaponColliderTransform = PlayerController.Instance.WeaponCollider;
            PlayerController.OnMouseMoveEventWithDirection += MouseFollowWithOffset;
        }

        private void OnDisable()
        {
            PlayerController.OnMouseMoveEventWithDirection -= MouseFollowWithOffset;
        }

        public void Attack()
        {
            _swordAnimator.SetTrigger((int)AttackHash);
            _weaponColliderTransform.gameObject.SetActive(true);
            _slashGameObject = Instantiate(slashEffectPrefab, PlayerController.Instance.SlashEffectSpawnLocation.position, Quaternion.identity);
            _slashGameObject.transform.SetParent(transform.parent);
        }

        public void DisableWeaponColliderAnimationEvent()
        {
            _weaponColliderTransform.gameObject.SetActive(false);
        }

        public PlayerWeaponScriptableObjects GetWeaponInfo()
        {
            return weaponInfo;
        }

        private void MouseFollowWithOffset(bool shouldFlip, float moveX, float moveY)
        {
            var angle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg;
            slashEffectPrefab.GetComponent<SpriteRenderer>().flipX = shouldFlip;
            _weaponColliderTransform.rotation = Quaternion.Euler(0, shouldFlip ? 180 : 0, 0);
            if (shouldFlip)
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            else
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SwingUpFlipAnimationEvent()
        {
            _slashGameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        }
    }
    
}