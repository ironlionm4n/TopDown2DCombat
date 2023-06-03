using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] private float fadeTime = .4f;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator SlowFadeRoutine()
    {
        var elapsedTime = 0f;
        var startAlpha = _spriteRenderer.color.a;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            var newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeTime);
            _spriteRenderer.color =
                new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, newAlpha);
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
