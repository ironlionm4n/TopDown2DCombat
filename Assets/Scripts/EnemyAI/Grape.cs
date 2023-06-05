using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;

    private Animator _grapeAnimator;
    private SpriteRenderer _spriteRenderer;
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        _grapeAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        _grapeAnimator.SetTrigger(AttackHash);

        _spriteRenderer.flipX = transform.position.x - PlayerController.Instance.transform.position.x > 0;
    }

    public void SpawnProjectileAnimationEvent()
    {
        Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}
