using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private enum AIState
    {
        Roaming
    }
    
    [SerializeField] private float moveDelay;

    private AIState _currentState;
    private EnemyPathfinding _enemyPathfinding;


    private void Awake()
    {
        _currentState = AIState.Roaming;
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (_currentState == AIState.Roaming)
        {
            _enemyPathfinding.SetMoveDirection(GetRoamingPosition());
            yield return new WaitForSeconds(moveDelay);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}