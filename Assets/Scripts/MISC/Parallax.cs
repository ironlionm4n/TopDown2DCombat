using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset;
    private Camera _camera;
    private Vector2 _startPosition;
    private Vector2 travelVector => (Vector2) _camera.transform.position - _startPosition;
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = _startPosition + travelVector * parallaxOffset;
    }
}
