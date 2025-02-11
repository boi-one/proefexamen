using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class CameraControl : MonoBehaviour
{
    [SerializeField]
    float zoomAmount;
    float zoom
    {
        get => Camera.main.fieldOfView;
        set
        {
            value = Mathf.Clamp(value, 5f, 100f);
            Camera.main.fieldOfView = value;
        }
    }
    void Update()
    {
        Zoom();
    }

    void Zoom()
    {
        zoom += Input.mouseScrollDelta.y != 0 
            ? Input.mouseScrollDelta.y > 0 
                ? -zoomAmount : zoomAmount : 0;
    }
}
