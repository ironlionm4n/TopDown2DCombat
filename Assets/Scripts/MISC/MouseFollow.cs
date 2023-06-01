using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.OnMouseMoveEventWithDirection += FaceMouse;
    }

    private void FaceMouse(bool flipx, float movex, float movey)
    {
        var mousePosition = new Vector3(movex, movey, 0);
        var mousePositionWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        var direction = (Vector2) transform.position - (Vector2) mousePositionWorldPoint;
        transform.right = -direction;
    }
}
