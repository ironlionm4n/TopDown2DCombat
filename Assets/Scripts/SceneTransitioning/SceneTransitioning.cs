using System.Collections;
using System.Collections.Generic;
using MISC;
using UnityEngine;

public class SceneTransitioning : Singleton<SceneTransitioning>
{
    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName)
    {
        SceneTransitionName = sceneTransitionName;
    }
}
