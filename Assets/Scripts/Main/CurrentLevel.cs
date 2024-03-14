using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    public GameObject Moves;
    public GameObject Time;
    public GameObject Goal;
    public GameObject BestScore;
    public TextMeshProUGUI GoalText;
    public TextMeshProUGUI MovesText;
    public TextMeshProUGUI TimeText;
    public Win CheckWin;
    public int MovesLeft = 45;
    public int TimeLeft = 60;
    public bool CoroutineStop;

    public void LevelOne()
    {
        GoalText.text = "2500";
        CheckWin.LevelOneCheck = true;
    }

    public void LevelTwo()
    {
        GoalText.text = "2500";
        CheckWin.LevelTwoCheck = true;
        Moves.SetActive(true);
        MovesText.text = $"{MovesLeft}";
    }

    public void LevelThree()
    {
        GoalText.text = "2500";
        CheckWin.LevelThreeCheck = true;
        Time.SetActive(true);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (TimeLeft != 0)
        {
            TimeText.text = $"{TimeLeft--}";
            yield return new WaitForSeconds(1);
            if (CoroutineStop)
                break;
        }
    }

    public void LevelFour()
    {
        GoalText.text = "3000";
        CheckWin.LevelFourCheck = true;
        Moves.SetActive(true);
        Time.SetActive(true);
        StartCoroutine(Timer());
    }

    public void EndlessMode()
    {
        Goal.SetActive(false);
        BestScore.SetActive(true);
    }
}