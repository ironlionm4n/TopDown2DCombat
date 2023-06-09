using System;
using System.Collections;
using System.Collections.Generic;
using MISC;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Inventory
{
    public class ActiveInventory : Singleton<ActiveInventory>
    {
        public PlayerWeaponScriptableObjects CurrentActiveInventorySlotWeapon { get; set; }
        private int _activeSlotIndexNumber = 0;
        private PlayerControls _playerControls;
        
        protected override void Awake()
        {
            base.Awake();
            _playerControls = new PlayerControls();
        }

        private void Start()
        {
            ToggleActiveHighlight(1);
        }

        private void OnEnable()
        {
            _playerControls.Enable();
            _playerControls.Inventory.Keyboard.performed += HandleInventoryKeyboard;
        }

        private void OnDisable()
        {
            if (_playerControls != null)
            {
                _playerControls.Inventory.Keyboard.performed -= HandleInventoryKeyboard;
                
            }
        }

        public void EquipStartingWeapon()
        {
            ToggleActiveHighlight(1);
        }

        private void HandleInventoryKeyboard(InputAction.CallbackContext obj)
        {
            ToggleActiveSlot((int)obj.ReadValue<float>());
        }

        private void ToggleActiveSlot(int index)
        {
            ToggleActiveHighlight(index);
        }

        private void ToggleActiveHighlight(int index)
        {
            _activeSlotIndexNumber = index - 1;

            foreach (Transform inventorySlot in transform) inventorySlot.GetChild(0).gameObject.SetActive(false);

            transform.GetChild(_activeSlotIndexNumber).GetChild(0).gameObject.SetActive(true);
            ChangeActiveWeapon();
        }

        private void ChangeActiveWeapon()
        {
            if (PlayerHealth.Instance.IsDead)
            {
                return;
            }
            
            if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
            {
                Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
            }
            var childTransform = transform.GetChild(_activeSlotIndexNumber);
            var inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
            var weaponInfo = inventorySlot.GetWeaponInfo;

            if (weaponInfo == null)
            {
                ActiveWeapon.Instance.WeaponNull();
                return;
            }

            var weaponToSpawn = weaponInfo.WeaponPrefab;
            /*CurrentActiveInventorySlotWeapon = transform.GetChild(_activeSlotIndexNumber).GetComponent<InventorySlot>().GetWeaponInfo;*/
            var activeWeaponTransform = ActiveWeapon.Instance.transform;
            var weaponSpawn = Instantiate(weaponToSpawn, activeWeaponTransform.position, Quaternion.identity);
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,0);
            weaponSpawn.transform.SetParent(activeWeaponTransform);
            ActiveWeapon.Instance.NewWeapon(weaponSpawn.GetComponent<MonoBehaviour>());
        }
    }
}