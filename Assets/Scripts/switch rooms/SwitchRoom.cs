using System.Collections;
using TMPro;
using UnityEngine;

public class SwitchRoom : SingletonMonobehaviour<SwitchRoom>
{
    [SerializeField]
    GameObject waitingRoom;
    [SerializeField]
    GameObject operationRoom;
    [SerializeField]
    GameObject scoreSystem;
    [SerializeField]
    GameObject mouth;

    public bool operationRoomActive
    {
        get => _operationRoomActive;
    }
    bool _operationRoomActive = false;

    public void EnterWaitingRoom()
    {
        MainMenu.reference.startGame = true;
        UIManager.reference.AnouncementText.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(TimerCoroutine());
    }

    public void EnterOperationRoom()
    {
        UIManager.reference.AnouncementText.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }


    public void SwitchRooms()
    {
        _operationRoomActive = !_operationRoomActive;
        
        waitingRoom.SetActive(!_operationRoomActive);
        operationRoom.SetActive(_operationRoomActive);
        mouth.SetActive(operationRoomActive);
        scoreSystem.SetActive(operationRoomActive);
    }

    IEnumerator TimerCoroutine()
    {
        for (int i = 5; i >= 0; i--)
        {
            UIManager.reference.AnouncementText.text = "Be ready, your patient is arriving\n";
            UIManager.reference.AnouncementText.text += i.ToString();
            yield return new WaitForSeconds(1);
        }
        Transition.reference.AddFunction(SwitchRooms);
        Transition.reference.AddFunction(EnterOperationRoom);
        Transition.reference.StartTransition();
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
