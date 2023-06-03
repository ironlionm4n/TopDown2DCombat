using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime;
    private float _laserRange;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider2D;
    private Vector2 _direction;
    private SpriteFade _spriteFade;
    private bool _isGrowing;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _spriteFade = GetComponent<SpriteFade>();
    }

    private void Start()
    {
        SetDirection();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Indestructible>() && !other.isTrigger)
        {
            _isGrowing = false;
        }
    }

    public void UpdateLaserRange(float range)
    {
        _laserRange = range;
        StartCoroutine(IncreaseLaserLength());
    }

    private IEnumerator IncreaseLaserLength()
    {
        var timePassed = 0f;
        _isGrowing = true;
        while (_spriteRenderer.size.x < _laserRange && _isGrowing)
        {
            timePassed += Time.deltaTime;
            var linearTime = timePassed / laserGrowTime;
            _spriteRenderer.size = new Vector2(Mathf.Lerp(1f, _laserRange, linearTime), 1f);
            _capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, _laserRange, linearTime),
                _capsuleCollider2D.size.y);
            _capsuleCollider2D.offset = new Vector2(
                (Mathf.Lerp(1f, _laserRange, linearTime) / 2f),
                _capsuleCollider2D.offset.y);
            yield return null;
        }

        StartCoroutine(_spriteFade.SlowFadeRoutine());
    }

    private void SetDirection()
    {
        var mousePosition = Input.mousePosition;
        var mousePositionWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        var direction = (Vector2)transform.position - (Vector2)mousePositionWorldPoint;
        transform.right = -direction;
    }
}