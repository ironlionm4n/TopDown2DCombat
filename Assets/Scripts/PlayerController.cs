using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private PlayerControls _playerControls;
    private Vector2 _movementDirection;
    private Rigidbody2D _playerRigidbody;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GatherPlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _playerRigidbody.MovePosition(_playerRigidbody.position + _movementDirection * (moveSpeed * Time.deltaTime));
    }

    private void GatherPlayerInput()
    {
        _movementDirection = _playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }
}