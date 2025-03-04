using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button startGameButton;

    public void StartGame()
    {
        startGameButton.gameObject.SetActive(false);
        Transition.reference.AddFunction(SwitchRoom.reference.EnterWaitingRoom);
        Transition.reference.StartTransition();
    }
}
