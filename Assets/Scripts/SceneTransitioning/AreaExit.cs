using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [SerializeField] [Tooltip("Name of portal entrance to spawn player at in next scene.")]
    private string sceneTransitionName;

    [SerializeField] private float sceneLoadDelay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            SceneTransitioning.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(sceneName);
    }
}