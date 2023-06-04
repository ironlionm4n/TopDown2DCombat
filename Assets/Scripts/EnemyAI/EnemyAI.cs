using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private enum AIState
    {
        Roaming,
        Attacking
    }

    [SerializeField] private float moveDelay;
    [SerializeField] private float attackRange;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown;
    [SerializeField] private bool movesWhileAttacking;

    private AIState _currentState;
    private EnemyPathfinding _enemyPathfinding;
    private Vector2 _currentRoamPosition;
    private float _timeRoaming = 0f;
    private bool _canAttack = true;

    private void Awake()
    {
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start()
    {
        UpdateState(AIState.Roaming);
        _currentRoamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (_currentState)
        {
            case AIState.Roaming:
                Roaming();
                break;
            case AIState.Attacking:
                Attacking();
                break;
            default: break;
        }
    }

    private Vector2 GetRoamingPosition()
    {
        _timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void Roaming()
    {
        _timeRoaming += Time.deltaTime;
        _enemyPathfinding.SetMoveDirection(_currentRoamPosition);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
            UpdateState(AIState.Attacking);

        if (_timeRoaming > moveDelay) _currentRoamPosition = GetRoamingPosition();
    }

    private void UpdateState(AIState newState)
    {
        _currentState = newState;
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            UpdateState(AIState.Roaming);
        }
        
        if (attackRange != 0 && _canAttack)
        {
            _canAttack = false;
            (enemyType as IEnemy)?.Attack();
            if (!movesWhileAttacking)
            {
                _enemyPathfinding.StopMoving();
            }
            else
            {
                _enemyPathfinding.SetMoveDirection(GetRoamingPosition());
            }
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }
}