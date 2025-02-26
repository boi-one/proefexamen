using UnityEngine;


public class CameraControl : Interaction
{
    #region Variables
    
    public static Transform pivotPoint;
    
    float pitch = 0;
    float yaw = 0;
    
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
    } float _positionValue;
    
    #endregion
    
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
        if (!transform.parent)
            transform.eulerAngles = new(0, 0, 0);

        if (pivotPoint && Input.GetMouseButtonDown(0))
        {
            transform.parent.eulerAngles = new(0, 0, 0);
            pitch = 0;
            yaw = 0;
        }
        
        transform.parent = pivotPoint;
        transform.position = pivotPoint ? pivotPoint.position - pivotPoint.forward * zoom : new(0,0,-1) * zoom;
        if (Input.GetMouseButton(1) && transform.parent)
        {
            Mathf.Clamp(pitch, -90, 90);
            Mathf.Clamp(yaw, -90, 90);
            pitch += -Input.mousePositionDelta.y;
            yaw += Input.mousePositionDelta.x;
            transform.parent.rotation = Quaternion.Euler(pitch, yaw, 0);
        }
    }
}
