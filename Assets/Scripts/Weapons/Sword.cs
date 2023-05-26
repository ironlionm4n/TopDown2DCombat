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

        private void Awake()
        {
            _swordAnimator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            PlayerController.OnPlayerAttackEvent += HandlePlayerAttack;
        }

        private void HandlePlayerAttack(InputAction.CallbackContext context)
        {
            _swordAnimator.SetTrigger(Attack);
        }
    }
}