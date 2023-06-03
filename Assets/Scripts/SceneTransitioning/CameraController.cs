using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using MISC;
using PlayerScripts;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Start()
    {
        SetPlayerAsCameraFollowTarget();
    }

    public void SetPlayerAsCameraFollowTarget()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachineVirtualCamera.m_Follow = PlayerController.Instance.gameObject.transform;
    }
}
