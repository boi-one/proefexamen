using UnityEngine;
using UnityEngine.UI;

public class MainMenu : SingletonMonobehaviour<MainMenu>
{
    public bool startGame = false;

    public void StartGame()
    {
        UIManager.reference.startGameButton.gameObject.SetActive(false);
        Transition.reference.AddFunction(SwitchRoom.reference.EnterWaitingRoom);
        Transition.reference.StartTransition();
    }
}
