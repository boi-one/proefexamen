using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    # region Variables
    
    public UnityEvent Win;
    public UnityEvent NoTimeLeft;
    Func<string> invokeNoTimeLeft => _invokeNoTimeLeft ??= () =>
    {
        NoTimeLeft.Invoke();
        return "0";
    }; Func<string> _invokeNoTimeLeft;
    
    [SerializeField]
    int difficultyMultiplier = 1;
    float scoreTimer => _scoreTimer -= Time.deltaTime / difficultyMultiplier / 2;
    float _scoreTimer = 100;
    float progress;
    
    Text scoreText => _scoreText ??= GetComponentInChildren<Text>();
    Text _scoreText;
    Slider progressBar => _progressBar ??= GetComponentInChildren<Slider>();
    Slider _progressBar;
    List<Affliction> maximumAmountDirt = new();
    
    #endregion

    void Awake()
    {
        maximumAmountDirt = Patient.instance.Parts.SelectMany(_ => _.Afflictions).Where(_ => _.Amount > 0).ToList();
        Win.AddListener(() => Transition.reference.AddFunction(() => SceneManager.LoadScene("Win")));
    } 

    void Update() => ScoreManager();

    void ScoreManager()
    {
        progress = maximumAmountDirt.Count(_ => _.Amount == 0) / (float)maximumAmountDirt.Count;
        scoreText.text = scoreTimer > 0 ? ((int)(difficultyMultiplier * scoreTimer)).ToString() : invokeNoTimeLeft();
        progressBar.value = progress;
        new Action(progress == 1 ? (Action)(() =>
        {
            Win.Invoke();
            this.enabled = false;
        }) : () => { }).Invoke();
    }
}