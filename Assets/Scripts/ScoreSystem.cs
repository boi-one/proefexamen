using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    int difficultyMultiplier = 1;
    float scoreTimer => _scoreTimer -= Time.deltaTime / difficultyMultiplier;
    float _scoreTimer = 100;
    
    Text scoreText => GetComponentInChildren<Text>();
    Slider progressBar => GetComponentInChildren<Slider>();

    float progress;
    [SerializeField]
    List<Transform> teeth = new();

    void Update()
    {
        ScoreManager();
    }

    void ScoreManager()
    {
        progress = teeth.Count(_ => _.GetComponent<Tooth>().clean) / (float)teeth.Count;
        scoreText.text = scoreTimer > 0 ? ((int)(difficultyMultiplier * scoreTimer)).ToString() : "0";
        progressBar.value = progress;
    } 
    
}