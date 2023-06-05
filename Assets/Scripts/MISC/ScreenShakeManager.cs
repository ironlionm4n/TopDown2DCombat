using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using MISC;
using UnityEngine;

public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    private CinemachineImpulseSource _impulseSource;

    protected override void Awake()
    {
        base.Awake();
        
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen()
    {
        _impulseSource.GenerateImpulse();
    }
}
