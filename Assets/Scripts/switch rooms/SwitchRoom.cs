using System.Collections;
using TMPro;
using UnityEngine;

public class SwitchRoom : MonoBehaviour
{
    public static SwitchRoom reference;

    [SerializeField]
    private GameObject waitingRoom;
    [SerializeField]
    private GameObject operationRoom;
    [SerializeField]
    private TMP_Text AnouncementText;

    public bool operationRoomActive
    {
        get => _operationRoomActive;
    }
    bool _operationRoomActive = false;

    void Awake()
    {
        reference = this;
    }

    public void EnterWaitingRoom()
    {
        WaitRoomCameraControls.reference.startGame = true;
        AnouncementText.gameObject.SetActive(true);
        StartCoroutine(TimerCoroutine());
    }

    public void EnterOperationRoom()
    {
        AnouncementText.gameObject.SetActive(false);
    }


    public void SwitchRooms()
    {
        _operationRoomActive = !_operationRoomActive;

        waitingRoom.SetActive(!_operationRoomActive);
        operationRoom.SetActive(_operationRoomActive);
    }

    IEnumerator TimerCoroutine()
    {
        for (int i = 5; i >= 0; i--)
        {
            AnouncementText.text = "Be ready, your patient is ariving\n";
            AnouncementText.text += i.ToString();
            yield return new WaitForSeconds(1);
        }
        SwitchRooms();
        EnterOperationRoom();
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
