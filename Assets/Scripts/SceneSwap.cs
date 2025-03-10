using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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

    public void SwitchToMainGame() 
    {
        reference.StartTransition();
        toMainGame.Invoke();
    } 

    public void SwitchToMainMenu()
    {
        reference.StartTransition();
        toMainMenu.Invoke();
    } 
}
