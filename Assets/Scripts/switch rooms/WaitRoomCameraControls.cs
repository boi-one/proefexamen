using UnityEngine;

public class WaitRoomCameraControls : SingletonMonobehaviour<WaitRoomCameraControls>
{
    [HideInInspector]
    public float yaw, pitch;
    void Update()
    {
        if (SwitchRoom.reference.operationRoomActive || !MainMenu.reference.startGame) return;
        yaw += Input.mousePositionDelta.x;
        pitch -= Input.mousePositionDelta.y;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.eulerAngles = Quaternion.Euler(pitch, yaw, 0).eulerAngles;
    }
}
