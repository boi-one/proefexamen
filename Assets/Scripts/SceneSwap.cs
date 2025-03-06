using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    public void SwitchToMainGame() => SceneManager.LoadScene("SampleScene");
    public void SwitchToMainMenu() => SceneManager.LoadScene("MainMenu");
}
