using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScoreText;
    public int bestScore;
    public int score = 0;

    private void Start()
    {
        UpdateScore(0);
        UploadData();
    }

    void UploadData()
    {
        string path = Application.persistentDataPath + "/scoresave.json";
        if (File.Exists(path))
        {
            string jsonUpload = File.ReadAllText(path);
            DataScore dataScore = new DataScore();
            dataScore = JsonUtility.FromJson<DataScore>(jsonUpload);
            bestScore = dataScore.bestScoreData;
            bestScoreText.text = bestScore.ToString();
        }
    }

    public void UpdateScore(int scorePoints)
    {
        score += scorePoints;
        currentScore.text = $"{score}";
    }
    
    public void BackToMenuButton()
    {
        if (score > bestScore)
        {
            DataScore dataScore = new DataScore();
            dataScore.bestScoreData = score;
            string json = JsonUtility.ToJson(dataScore);
            File.WriteAllText(Application.persistentDataPath + "/scoresave.json", json);
        }
        SceneManager.LoadScene(0);
    }

    [Serializable] public class DataScore
    {
        public int bestScoreData;
    }
}