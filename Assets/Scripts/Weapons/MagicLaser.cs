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

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        SetDirection();
    }

    public void UpdateLaserRange(float range)
    {
        _laserRange = range;
        StartCoroutine(IncreaseLaserLength());
    }

    private IEnumerator IncreaseLaserLength()
    {
        var timePassed = 0f;
        while (_spriteRenderer.size.x < _laserRange)
        {
            timePassed += Time.deltaTime;
            var linearTime = timePassed / laserGrowTime;
            _spriteRenderer.size = new Vector2(Mathf.Lerp(1f, _laserRange, linearTime), 1f);
            _capsuleCollider2D.size = new Vector2(Mathf.Lerp(_capsuleCollider2D.size.x, _laserRange, linearTime),
                _capsuleCollider2D.size.y);
            _capsuleCollider2D.offset = new Vector2(Mathf.Lerp(_capsuleCollider2D.offset.x, _laserRange, linearTime),
                _capsuleCollider2D.size.y);
            yield return null;
        }
    }

    private void SetDirection()
    {
        var mousePosition = Input.mousePosition;
        var mousePositionWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        var direction = (Vector2)transform.position - (Vector2)mousePositionWorldPoint;
        transform.right = -direction;
    }
}