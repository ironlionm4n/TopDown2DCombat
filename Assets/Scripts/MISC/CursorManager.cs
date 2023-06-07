using System;
using UnityEngine;
using UnityEngine.UI;
public class CursorManager : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        Cursor.visible = false;
        if (Application.isEditor)
        {
            Cursor.lockState = CursorLockMode.None;
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void Update()
    {
        var mousePosition = Input.mousePosition;
        _image.rectTransform.position = mousePosition;
    }
}
