using UnityEngine;

public class WaitRoomCameraControls : SingletonMonobehaviour<WaitRoomCameraControls>
{
    [HideInInspector]
    public float yaw, pitch;
    public static bool staystill = true;
    void Update()
    {
        if (staystill) return;
        yaw += Input.mousePositionDelta.x;
        pitch -= Input.mousePositionDelta.y;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.eulerAngles = Quaternion.Euler(pitch, yaw, 0).eulerAngles;
    }
}
