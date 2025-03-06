using System.Collections;
using TMPro;
using UnityEngine;

public class SwitchRoom : SingletonMonobehaviour<SwitchRoom>
{
    [SerializeField]
    GameObject waitingRoom;
    [SerializeField]
    GameObject operationRoom;

    public bool operationRoomActive
    {
        get => _operationRoomActive;
    }
    bool _operationRoomActive = false;

    public void EnterWaitingRoom()
    {
        MainMenu.reference.startGame = true;
        UIManager.reference.AnouncementText.gameObject.SetActive(true);
        StartCoroutine(TimerCoroutine());
    }

    public void EnterOperationRoom()
    {
        UIManager.reference.AnouncementText.gameObject.SetActive(false);
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
            UIManager.reference.AnouncementText.text = "Be ready, your patient is ariving\n";
            UIManager.reference.AnouncementText.text += i.ToString();
            yield return new WaitForSeconds(1);
        }
        SwitchRooms();
        EnterOperationRoom();
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
