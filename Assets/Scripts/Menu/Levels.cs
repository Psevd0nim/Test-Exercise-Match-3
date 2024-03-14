using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public GameObject LoadingScreen;

    public void LevelOne()
    {
        DataPersistence.Instance.LevelNumber = 1;
        StartCoroutine(LoadYourAsyncScene());
    }
    public void LevelTwo() 
    {
        DataPersistence.Instance.LevelNumber = 2;
        StartCoroutine(LoadYourAsyncScene());
    }
    public void LevelThree() 
    {
        DataPersistence.Instance.LevelNumber = 3;
        StartCoroutine(LoadYourAsyncScene());
    }
    public void LevelFour() 
    {
        DataPersistence.Instance.LevelNumber = 4;
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        gameObject.SetActive(false);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        LoadingScreen.SetActive(true);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
