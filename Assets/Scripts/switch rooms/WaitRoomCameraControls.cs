using UnityEngine;

public class WaitRoomCameraControls : MonoBehaviour
{
    public float yaw, pitch;
    public static WaitRoomCameraControls reference;

    void Awake()
    {
        reference = this;        
    }

    public bool startGame = false; //temp value for testing


    void Update()
    {
        if (SwitchRoom.reference.operationRoomActive || !startGame) return;
        
        yaw += Input.mousePositionDelta.x;
        pitch -= Input.mousePositionDelta.y;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        Camera.main.transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
