using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string sceneTransitionName;

    private void OnEnable()
    {
        if (sceneTransitionName.Equals(SceneTransitioning.Instance.SceneTransitionName))
        {
            UIFade.Instance.FadeToClear();
            PlayerController.Instance.transform.position = transform.position;
            CameraController.Instance.SetPlayerAsCameraFollowTarget();
        }
    }
}
