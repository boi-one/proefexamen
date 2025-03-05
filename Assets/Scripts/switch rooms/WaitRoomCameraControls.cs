using UnityEngine;

public class WaitRoomCameraControls : SingletonMonobehaviour<WaitRoomCameraControls>
{
    public float yaw, pitch;

    void Awake()
    {
        Debug.Log("awake");
    }

    void Update()
    {
        if (SwitchRoom.reference.operationRoomActive || !MainMenu.reference.startGame) return;
        
        yaw += Input.mousePositionDelta.x;
        pitch -= Input.mousePositionDelta.y;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        Camera.main.transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
