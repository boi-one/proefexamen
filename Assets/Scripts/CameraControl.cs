using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class CameraControl : Interaction
{
    Vector3 defaultCamPos = new Vector3(0, 1, -10);
    public static Transform pivotPoint;
    bool boolCheck = true;
    
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
        transform.parent = pivotPoint;
        // transform.eulerAngles = new Vector3(0, 0, 0);
        transform.position = pivotPoint ? pivotPoint.position - pivotPoint.forward * zoom : new Vector3(0,0,-1) * zoom;
        
        if (Input.GetMouseButton(1))
            /*transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, 
                Quaternion.Euler(-Input.mousePositionDelta.y, Input.mousePositionDelta.x, 0),
                50);*/

            // transform.parent.rotation = Quaternion.Euler(transform.parent.eulerAngles +
            //                                          new Vector3(-Input.mousePositionDelta.y,
            //                                              Input.mousePositionDelta.x, 0));


            transform.parent.eulerAngles += new Vector3(-Input.mousePositionDelta.y, Input.mousePositionDelta.x, 0);
    }
}
