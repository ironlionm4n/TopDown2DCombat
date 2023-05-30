using System.Collections;
using System.Collections.Generic;
using MISC;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace UI
{
    public class UIFade : Singleton<UIFade>
    {
        [SerializeField] private Image fadeScreen;
        [SerializeField] private float fadeSpeed;
        [SerializeField] private float maxFadeDelta;
        
        private IEnumerator _fadeRoutine;

        public void FadeToBlack()
        {
            if (_fadeRoutine != null)
            {
                StopCoroutine(_fadeRoutine);
            }

            _fadeRoutine = FadeRoutine(255f);
            StartCoroutine(_fadeRoutine);
        }

        public void FadeToClear()
        {
            if (_fadeRoutine != null)
            {
                StopCoroutine(_fadeRoutine);
            }

            _fadeRoutine = FadeRoutine(0f);
            StartCoroutine(_fadeRoutine);
        }
        
        private IEnumerator FadeRoutine(float targetAlpha)
        {
            while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
            {
                var currentColor = fadeScreen.color;
                maxFadeDelta = 5f;
                var alpha = Mathf.MoveTowards(currentColor.a, targetAlpha, maxFadeDelta * Time.deltaTime);
                fadeScreen.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                yield return null;
            }
        }
    }
}