using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreSystem : SingletonMonobehaviour<ScoreSystem>
{
    # region Variables
    
    Transition reference;
    public UnityEvent Angry;
    public UnityEvent Win;
    public UnityEvent NoTimeLeft;
    Func<string> invokeNoTimeLeft => _invokeNoTimeLeft ??= () =>
    {
        reference.StartTransition();
        NoTimeLeft.Invoke();
        this.enabled = false;
        return "0";
    }; Func<string> _invokeNoTimeLeft;

    public int difficultyMultiplier = 1;
    float scoreTimer => _scoreTimer -= Time.deltaTime / difficultyMultiplier / 2;
    float _scoreTimer = 100;
    float progress;

    Text scoreText => _scoreText ??= GetComponentInChildren<Text>(true);
    Text _scoreText;
    Slider progressBar => _progressBar ??= GetComponentInChildren<Slider>(true);
    Slider _progressBar;
    List<Affliction> maximumAmountDirt = new();
    float minimumFilth = 0.4f;
    #endregion

    void Awake()
    {
        FindObjectsByType<Transition>(FindObjectsSortMode.None).FirstOrDefault(_ => reference = _);
        maximumAmountDirt = Patient.reference.Parts.SelectMany(_ => _.Afflictions).Where(_ => _.Amount > minimumFilth).ToList();
        Win.AddListener(() => reference.AddFunction(() => SceneManager.LoadScene("Win")));
        NoTimeLeft.AddListener(() => reference.AddFunction(() => SceneManager.LoadScene("Lose")));
    } 

    void Update() => ScoreManager();

    bool gotAngry = false;
    void ScoreManager()
    {
        progress = maximumAmountDirt.Count(_ => _.Amount <= minimumFilth) / (float)maximumAmountDirt.Count;
        scoreText.text = scoreTimer > 0 ? ((int)(difficultyMultiplier * scoreTimer)).ToString() : invokeNoTimeLeft();
        if (scoreTimer < 60 && !gotAngry)
        {
            Angry.Invoke();
            gotAngry = true;
        }
        if (scoreTimer > 65)
            gotAngry = false;
        progressBar.value = progress;
        new Action(progress == 1 ? (Action)(() =>
        {
            reference.StartTransition();
            Win.Invoke();
            this.enabled = false;
        }) : () => { }).Invoke();
    }
}
