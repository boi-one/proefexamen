using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using System.Collections;


public class SceneSwap : MonoBehaviour
{
    public UnityEvent toMainGame;
    public UnityEvent toMainMenu;
    public GameObject TransitionObject;
    Transition reference;
    void Awake()
    {
        bool checkForMultiple = FindObjectsByType<Transition>(FindObjectsSortMode.None).Any();
        if (!checkForMultiple)
            Instantiate(TransitionObject);
        
        FindObjectsByType<Transition>(FindObjectsSortMode.None).FirstOrDefault(_ => reference = _);
        toMainGame.AddListener(() => reference.AddFunction(() => SceneManager.LoadScene("SampleScene")));
        toMainMenu.AddListener(() => reference.AddFunction(() => SceneManager.LoadScene("MainMenu")));
    }
    
    public void EnterWaitingRoom()
    {
        Cursor.lockState = CursorLockMode.Locked;
        WaitRoomCameraControls.staystill = false;
        StartCoroutine(TimerCoroutine());
    }
            
    IEnumerator TimerCoroutine()
    {
        UIManager.reference.AnouncementText.gameObject.SetActive(true);
        UIManager.reference.startGameButton.gameObject.SetActive(false);
        UIManager.reference.difficultyDropdown.gameObject.SetActive(false);
        
        for (int i = 5; i >= 0; i--)
        {
            UIManager.reference.AnouncementText.text = "Be ready, your patient is arriving\n";
            UIManager.reference.AnouncementText.text += i.ToString();
            yield return new WaitForSeconds(1);
        }
        WaitRoomCameraControls.staystill = true;
        Cursor.lockState = CursorLockMode.None;
        reference.StartTransition();
        toMainGame.Invoke();
    }
    
    public void SwitchToMainGame() 
    {
        WaitRoomCameraControls.staystill = true;
        EnterWaitingRoom();
    } 

    public void SwitchToMainMenu()
    {
        reference.StartTransition();
        toMainMenu.Invoke();
    } 
}
