using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMonobehaviour<UIManager>
{

    public Button startGameButton;
    public TMP_Text AnouncementText;
    public TMP_Dropdown difficultyDropdown;
    public int difficultyMultiplier
    {
        get => ScoreSystem.reference.difficultyMultiplier;
        set => ScoreSystem.reference.difficultyMultiplier = value;
    }

    void Awake()
    {
        difficultyDropdown.onValueChanged.AddListener(SwitchDifficulty);
    }

    public void SwitchDifficulty(int i)
    {
        difficultyMultiplier = i switch
        {
            0 => 3,
            1 => 2,
            2 => 1,
            _ => throw new("invalid")
        };
    }
}
