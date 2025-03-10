using UnityEngine;
using UnityEngine.UI;

public class MainMenu : SingletonMonobehaviour<MainMenu>
{
    public bool startGame = false;

    public void StartGame()
    {
        startGame = true;
        UIManager.reference.startGameButton.gameObject.SetActive(false);
        UIManager.reference.difficultyDropdown.gameObject.SetActive(false);
        Transition.reference.AddFunction(SwitchRoom.reference.EnterWaitingRoom);
        Transition.reference.StartTransition(color: new Vector3(1, 1, 1), loading: false);
    }
}
