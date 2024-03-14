using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    public GameObject Moves;
    public GameObject Time;
    public TextMeshProUGUI Goal;
    public TextMeshProUGUI MovesText;
    public TextMeshProUGUI TimeText;
    public Win CheckWin;
    public int MovesLeft = 45;
    public int TimeLeft = 60;
    public bool CoroutineStop;

    public void LevelOne()
    {
        Goal.text = "2500";
        CheckWin.LevelOneCheck = true;
    }

    public void LevelTwo()
    {
        Goal.text = "2500";
        CheckWin.LevelTwoCheck = true;
        Moves.SetActive(true);
        MovesText.text = $"{MovesLeft}";
    }

    public void LevelThree()
    {
        Goal.text = "2500";
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
        Goal.text = "3000";
        CheckWin.LevelFourCheck = true;
        Moves.SetActive(true);
        Time.SetActive(true);
        StartCoroutine(Timer());
    }

    public void EndlessMode()
    {

    }
}
