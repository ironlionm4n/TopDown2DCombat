using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory
{
    
    public class ActiveInventory : MonoBehaviour
    {
        private int _activeSlotIndexNumber = 0;

        private PlayerControls _playerControls;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            _playerControls.Enable();
            _playerControls.Inventory.Keyboard.performed += HandleInventoryKeyboard;
        }

        private void OnDisable()
        {
            _playerControls.Inventory.Keyboard.performed -= HandleInventoryKeyboard;
        }

        private void HandleInventoryKeyboard(InputAction.CallbackContext obj)
        {
            ToggleActiveSlot((int) obj.ReadValue<float>());
        }

        private void ToggleActiveSlot(int index)
        {
            ToggleActiveHighlight(index);
        }

        private void ToggleActiveHighlight(int index)
        {
            _activeSlotIndexNumber = index - 1;

            foreach (Transform inventorySlot in transform)
            {
                inventorySlot.GetChild(0).gameObject.SetActive(false);
            }
            
            transform.GetChild(_activeSlotIndexNumber).GetChild(0).gameObject.SetActive(true);
        }
    }
}
