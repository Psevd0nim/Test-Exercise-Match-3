using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    public TextMeshProUGUI Goal;
    public Win CheckWin;

    public void LevelOne()
    {
        Goal.text = "2500";
        CheckWin.LevelOneCheck = true;
    }

    public void LevelTwo()
    {
        Goal.text = "6000";
    }

    public void LevelThree()
    {
        Goal.text = "7000";
    }

    public void LevelFour()
    {
        Goal.text = "8000";
    }

    public void EndlessMode()
    {

    }
}
