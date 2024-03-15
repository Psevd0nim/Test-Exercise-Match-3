using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public bool LevelOneCheck;
    public bool LevelTwoCheck;
    public bool LevelThreeCheck;
    public bool LevelFourCheck;
    private MainSceneUI MainSceneUI;
    private GameManager gameManager;
    public GameObject WinObject;
    public CurrentLevel CurrentLevel;
    public GameObject GameOverObject;

    private void Start()
    {
        MainSceneUI = GameObject.Find("Canvas").GetComponent<MainSceneUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (LevelOneCheck) LevelOne();
        if (LevelTwoCheck) LevelTwo();
        if (LevelThreeCheck) LevelThree();
        if (LevelFourCheck) LevelFour();
    }

    void LevelOne()
    {
        if(MainSceneUI.score >= 2500)
        {
            WinObject.SetActive(true);
            gameManager.GameOverOrWin = true;
        }
    }

    void LevelTwo()
    {
        if (MainSceneUI.score >= 2500)
        {
            WinObject.SetActive(true);
            gameManager.GameOverOrWin = true;
        }
        else if(CurrentLevel.MovesLeft == 0)
        {
            GameOverObject.SetActive(true);
        }
        
    }

    void LevelThree()
    {
        if (MainSceneUI.score >= 2500)
        {
            WinObject.SetActive(true);
            gameManager.GameOverOrWin = true;
            CurrentLevel.CoroutineStop = true;
        }
        if (CurrentLevel.TimeLeft == -1)
        {
            GameOverObject.SetActive(true);
            CurrentLevel.CoroutineStop = true;
        }
    }

    void LevelFour()
    {
        if (MainSceneUI.score >= 3000)
        {
            WinObject.SetActive(true);
            gameManager.GameOverOrWin = true;
            CurrentLevel.CoroutineStop = true;
        }
        else if(CurrentLevel.MovesLeft == 0 || CurrentLevel.TimeLeft == -1)
        {
            GameOverObject.SetActive(true);
            CurrentLevel.CoroutineStop = true;
        }
    }
}