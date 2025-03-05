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
    
    public UnityEvent Angry;
    public UnityEvent Win;
    public UnityEvent NoTimeLeft;
    Func<string> invokeNoTimeLeft => _invokeNoTimeLeft ??= () =>
    {
        NoTimeLeft.Invoke();
        this.enabled = false;
        return "0";
    }; Func<string> _invokeNoTimeLeft;

    public int difficultyMultiplier = 1;
    float scoreTimer => _scoreTimer -= Time.deltaTime / difficultyMultiplier / 2;
    float _scoreTimer = 100;
    float progress;

    Image popUpImage;
    Text scoreText => _scoreText ??= GetComponentInChildren<Text>();
    Text _scoreText;
    Slider progressBar => _progressBar ??= GetComponentInChildren<Slider>();
    Slider _progressBar;
    List<Affliction> maximumAmountDirt = new();
    
    #endregion

    void Awake()
    {
        popUpImage = FindObjectsByType<Image>(FindObjectsSortMode.None).FirstOrDefault(_ => _.name == "Angry Icon");
        Angry.AddListener(() => StartCoroutine(TimerCoroutine(1)));
        maximumAmountDirt = Patient.instance.Parts.SelectMany(_ => _.Afflictions).Where(_ => _.Amount > 0).ToList();
        Win.AddListener(() => Transition.reference.AddFunction(() => SceneManager.LoadScene("Win")));
        NoTimeLeft.AddListener(() => Transition.reference.AddFunction(() => SceneManager.LoadScene("Lose")));
    } 

    void Update() => ScoreManager();

    IEnumerator TimerCoroutine(float time)
    {
        _scoreTimer -= 5;
        popUpImage.enabled = true;
        yield return new WaitForSeconds(time);
        popUpImage.enabled = false;
    }

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
