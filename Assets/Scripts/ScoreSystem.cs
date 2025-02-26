using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    # region Variables
    
    UnityEvent Win;
    UnityEvent NoTimeLeft;
    Func<string> invokeNoTimeLeft => _invokeNoTimeLeft ??= () =>
    {
        NoTimeLeft.Invoke();
        return "0";
    }; Func<string> _invokeNoTimeLeft;
    
    [SerializeField]
    int difficultyMultiplier = 1;
    float scoreTimer => _scoreTimer -= Time.deltaTime / difficultyMultiplier;
    float _scoreTimer = 100;
    float progress;
    
    [SerializeField]
    List<Transform> teeth = new();
    Text scoreText => GetComponentInChildren<Text>();
    Slider progressBar => GetComponentInChildren<Slider>();

    #endregion

    void Update() => ScoreManager();

    void ScoreManager()
    {
        progress = teeth.Count(_ => _.GetComponent<Tooth>().clean) / (float)teeth.Count;
        scoreText.text = scoreTimer > 0 ? ((int)(difficultyMultiplier * scoreTimer)).ToString() : invokeNoTimeLeft();
        progressBar.value = progress;
        new Action(progress == 1 ? () => Win.Invoke() : () => { }).Invoke();
    }
}