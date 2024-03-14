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

    private void Start()
    {
        MainSceneUI = GameObject.Find("Canvas").GetComponent<MainSceneUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (LevelOneCheck)
            LevelOne();
    }

    void LevelOne()
    {
        if(MainSceneUI.score > 2500)
        {
            WinObject.SetActive(true);
            gameManager.GameOverOrWin = true;
        }
    }
}