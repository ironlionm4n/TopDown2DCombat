using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private Rigidbody2D _slimeRigidbody;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _slimeRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _slimeRigidbody.MovePosition(_slimeRigidbody.position + _moveDirection * (moveSpeed * Time.deltaTime));
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        _moveDirection = moveDirection;
    }
}
