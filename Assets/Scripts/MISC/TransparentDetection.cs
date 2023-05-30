using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [SerializeField, Range(0f,.99f)] private float targetTransparencyAmount;
    [SerializeField] private float fadeDuration;

    private SpriteRenderer _spriteRenderer;
    private Tilemap _tilemap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) StartCoroutine(FadeToTransparency());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) StartCoroutine(FadeFromTransparency());
    }

    private IEnumerator FadeToTransparency()
    {
        var elapsedTime = 0f;

        if (_spriteRenderer != null)
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                var currentSpriteColor = _spriteRenderer.color;
                var adjustedAlpha = Mathf.Lerp(currentSpriteColor.a, targetTransparencyAmount,
                    elapsedTime / fadeDuration);
                _spriteRenderer.color = new Color(currentSpriteColor.r, currentSpriteColor.g, currentSpriteColor.b,
                    adjustedAlpha);
                yield return null;
            }
        else
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                var currentTilemapColor = _tilemap.color;
                var adjustedAlpha = Mathf.Lerp(currentTilemapColor.a, targetTransparencyAmount,
                    elapsedTime / fadeDuration);
                _tilemap.color = new Color(currentTilemapColor.r, currentTilemapColor.g, currentTilemapColor.b,
                    adjustedAlpha);
                yield return null;
            }
    }

    private IEnumerator FadeFromTransparency()
    {
        var elapsedTime = 0f;

        if (_spriteRenderer != null)
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                var currentSpriteColor = _spriteRenderer.color;
                var adjustedAlpha = Mathf.Lerp(currentSpriteColor.a, 1,
                    elapsedTime / fadeDuration);
                _spriteRenderer.color = new Color(currentSpriteColor.r, currentSpriteColor.g, currentSpriteColor.b,
                    adjustedAlpha);
                yield return null;
            }
        else
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                var currentTilemapColor = _tilemap.color;
                var adjustedAlpha = Mathf.Lerp(currentTilemapColor.a, 1,
                    elapsedTime / fadeDuration);
                _tilemap.color = new Color(currentTilemapColor.r, currentTilemapColor.g, currentTilemapColor.b,
                    adjustedAlpha);
                yield return null;
            }
    }
}