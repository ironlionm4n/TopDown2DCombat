using System;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerVisualController : MonoBehaviour
    {
        private Animator _playerAnimator;
        private SpriteRenderer _playerSpriteRenderer;

        // static readonly field, only calculated one time when class is first used
        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");

        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
            _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerMoveEvent += SetMovementVector;
            PlayerController.OnMouseMoveEvent += HandleSpriteFlip;
        }

        public void SetMovementVector(float moveX, float moveY)
        {
            if (_playerAnimator != null)
            {
                _playerAnimator.SetFloat(MoveX, moveX);
                _playerAnimator.SetFloat(MoveY, moveY);
            }
        }

        private void HandleSpriteFlip(bool shouldFlip)
        {
            if(_playerSpriteRenderer != null)
            {
                _playerSpriteRenderer.flipX = shouldFlip;
            }
        }
    }
}