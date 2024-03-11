using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] protected TextMeshProUGUI bestScore;
    private int score = 0;

    private void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int scorePoints)
    {
        score += scorePoints;
        currentScore.text = $"Current Score : {score}";
    }
    
    public void BackToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}