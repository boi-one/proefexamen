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
    
    [SerializeField]
    GameObject HurryUp;
    
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

    [SerializeField]
    GameObject popUpImage;
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
        Angry.AddListener(() => StartCoroutine(TimerCoroutine(1)));
        maximumAmountDirt = Patient.reference.Parts.SelectMany(_ => _.Afflictions).Where(_ => _.Amount > minimumFilth).ToList();
        Win.AddListener(() => reference.AddFunction(() => SceneManager.LoadScene("Win")));
        NoTimeLeft.AddListener(() => reference.AddFunction(() => SceneManager.LoadScene("Lose")));
    }

    void Update() => ScoreManager();

    IEnumerator TimerCoroutine(float time)
    {
        _scoreTimer -= 2;
        popUpImage.SetActive(true);
        yield return new WaitForSeconds(time);
        popUpImage.SetActive(false);
    }

    void ScoreManager()
    {
        progress = maximumAmountDirt.Count(_ => _.Amount <= minimumFilth) / (float)maximumAmountDirt.Count;
        scoreText.text = scoreTimer > 0 ? ((int)(difficultyMultiplier * scoreTimer)).ToString() : invokeNoTimeLeft();
        progressBar.value = progress;

        HurryUp.SetActive(Convert.ToInt32(scoreText.text) <= 60);
        
        new Action(progress == 1 ? (Action)(() =>
        {
            reference.StartTransition();
            Win.Invoke();
            this.enabled = false;
        }) : () => { }).Invoke();
    }
}
