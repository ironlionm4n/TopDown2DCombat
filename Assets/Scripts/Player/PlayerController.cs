using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        
        private PlayerControls _playerControls;
        private Vector2 _movementDirection;
        private Vector2 _mousePosition;
        private Rigidbody2D _playerRigidbody;

        public delegate void PlayerEvent(float moveX, float moveY);
        public delegate void PlayerAttackEvent(InputAction.CallbackContext context);
        public delegate void MouseMoveEvent(bool flipX);


        public static event PlayerEvent OnPlayerMoveEvent;
        public static event MouseMoveEvent OnMouseMoveEvent;
        public static event PlayerAttackEvent OnPlayerAttackEvent;
        
        private void Awake()
        {
            _playerControls = new PlayerControls();
            _playerRigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable()
        {
            _playerControls.Enable();
            _playerControls.Attack.PrimaryAttack.started += HandlePrimaryAttack;
        }

        private void Update()
        {
            GatherPlayerInput();
            ReadMousePosition();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void GatherPlayerInput()
        {
            _movementDirection = _playerControls.Movement.Move.ReadValue<Vector2>().normalized;
        }

        /// <summary>
        /// Retrieve the Mouse absolute position, compare with players screen point, if delta is greater is less than 0 then the sprite should flip
        /// </summary>
        private void ReadMousePosition()
        {
            var mousePosition = _playerControls.MousePosition.MousePosition.ReadValue<Vector2>();
            var playerScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            OnMouseMoveEvent?.Invoke(mousePosition.x < playerScreenPoint.x);
        }
        
        private void MovePlayer()
        {
            _playerRigidbody.MovePosition(_playerRigidbody.position +
                                          _movementDirection * (moveSpeed * Time.deltaTime));
            
            OnPlayerMoveEvent?.Invoke(_movementDirection.x, _movementDirection.y);
        }

        private void HandlePrimaryAttack(InputAction.CallbackContext context)
        {
            OnPlayerAttackEvent?.Invoke(context);
        }
    }
}