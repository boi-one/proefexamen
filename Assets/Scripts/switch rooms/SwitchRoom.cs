using System;
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
}
