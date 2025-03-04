using UnityEngine;

public class testscribt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transition.reference.AddFunction(SwitchRoom.reference.EnterWaitingRoom);
        Transition.reference.StartTransition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transition.reference.AddFunction(SwitchRoom.reference.SwitchRooms);
            Transition.reference.StartTransition(new Vector3(1, 1, 1), 8, 1);
        }
    }
}
