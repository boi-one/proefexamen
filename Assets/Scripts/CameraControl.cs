using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class CameraControl : Interaction
{
    public static Transform pivotPoint;
    bool boolCheck = true;
    
    float pitch = 0;
    float yaw = 0;
    
    float timer = 0;
    [SerializeField]
    float zoomAmount;
    float zoom
    {
        get => _positionValue;
        set
        {
            value = Mathf.Clamp(value, 1f, 10f);
            _positionValue = value;
        }
    }

    float _positionValue;
    
    void Update()
    {
        Zoom();
        CameraPivot();
    }

    void Zoom() => zoom += Input.mouseScrollDelta.y != 0 
            ? Input.mouseScrollDelta.y > 0 
                ? -zoomAmount 
                : zoomAmount 
            : 0;

    void CameraPivot()
    {
        // if (!transform.parent)
        // {
        //     pivotPoint.eulerAngles = new Vector3(0, 0, 0);
        //     transform.eulerAngles = new Vector3(0, 0, 0);
        // } fix this shit tuesday
        transform.parent = pivotPoint;
        transform.position = pivotPoint ? pivotPoint.position - pivotPoint.forward * zoom : new Vector3(0,0,-1) * zoom;
        if (Input.GetMouseButton(1) && transform.parent != null)
        {
            Mathf.Clamp(pitch, -90, 90);
            Mathf.Clamp(yaw, -90, 90);
            pitch += -Input.mousePositionDelta.y;
            yaw += Input.mousePositionDelta.x;
            transform.parent.rotation = Quaternion.Euler(pitch, yaw, 0);
        }
    }
}
