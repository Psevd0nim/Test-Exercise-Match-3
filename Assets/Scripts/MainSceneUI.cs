using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour
{
    public void BackToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}