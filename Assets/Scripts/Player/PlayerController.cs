using System.Collections;
using MISC;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float dashMultiplier;
        [SerializeField] private float dashTime;
        [SerializeField] private TrailRenderer playerTrailRenderer;
        [SerializeField] private float dashCooldown;

        private PlayerControls _playerControls;
        private Vector2 _movementDirection;
        private Vector2 _mousePosition;
        private Rigidbody2D _playerRigidbody;
        private bool _isDashing;
        private bool _canDash = true;
        private float _defaultMoveSpeed, _dashMoveSpeed;


        public delegate void PlayerEvent(float moveX, float moveY);

        public delegate void PlayerAttackEvent(InputAction.CallbackContext context, Transform playerTransform);

        public delegate void PlayerAttackCancelledEvent(InputAction.CallbackContext context);

        public delegate void MouseMoveEvent(bool flipX);

        public delegate void MouseMoveEventWithDirection(bool flipX, float moveX, float moveY);


        public static event PlayerEvent OnPlayerMoveEvent;
        public static event MouseMoveEvent OnMouseMoveEvent;
        public static event MouseMoveEventWithDirection OnMouseMoveEventWithDirection;
        public static event PlayerAttackEvent OnPlayerAttackEvent;
        public static event PlayerAttackCancelledEvent OnPlayerAttackCancelledEvent;

        protected override void Awake()
        {
            base.Awake();

            _playerControls = new PlayerControls();
            _playerRigidbody = GetComponent<Rigidbody2D>();
            _defaultMoveSpeed = moveSpeed;
            _dashMoveSpeed = _defaultMoveSpeed * dashMultiplier;
        }

        private void OnEnable()
        {
            if (_playerControls != null)
            {
                _playerControls.Enable();
                _playerControls.Attack.PrimaryAttack.started += HandlePrimaryAttack;
                _playerControls.Attack.PrimaryAttack.canceled += HandlePrimaryAttackCancelled;
                _playerControls.Movement.Dash.started += HandleDash;
            }
        }

        private void HandleDash(InputAction.CallbackContext obj)
        {
            if (!_canDash) return;

            _canDash = false;
            _isDashing = true;
            moveSpeed = _dashMoveSpeed;
            playerTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }

        private IEnumerator EndDashRoutine()
        {
            yield return new WaitForSeconds(dashTime);
            playerTrailRenderer.emitting = false;
            moveSpeed = _defaultMoveSpeed;
            _isDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            _canDash = true;
        }

        private void OnDisable()
        {
            if (_playerControls != null)
            {
                _playerControls.Attack.PrimaryAttack.started -= HandlePrimaryAttack;
                _playerControls.Attack.PrimaryAttack.canceled -= HandlePrimaryAttackCancelled;
                _playerControls.Movement.Dash.started -= HandleDash;
                _playerControls.Disable();
            }
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
            if (_isDashing) return;

            var mousePosition = _playerControls.MousePosition.MousePosition.ReadValue<Vector2>();
            var playerScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            var shouldFlip = mousePosition.x < playerScreenPoint.x;
            OnMouseMoveEvent?.Invoke(shouldFlip);
            OnMouseMoveEventWithDirection?.Invoke(shouldFlip, mousePosition.x, mousePosition.y);
        }

        private void MovePlayer()
        {
            _playerRigidbody.MovePosition(_playerRigidbody.position +
                                          _movementDirection * (moveSpeed * Time.deltaTime));

            OnPlayerMoveEvent?.Invoke(_movementDirection.x, _movementDirection.y);
        }

        private void HandlePrimaryAttack(InputAction.CallbackContext context)
        {
            OnPlayerAttackEvent?.Invoke(context, transform);
        }

        private void HandlePrimaryAttackCancelled(InputAction.CallbackContext context)
        {
            OnPlayerAttackCancelledEvent?.Invoke(context);
        }
    }
}