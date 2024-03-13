using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LoadingScreen;
    public GameObject Settings;
    private SettingsData settingsData;

    private void Start()
    {
        settingsData = GameObject.Find("SettingsData").GetComponent<SettingsData>();
        UploadData();
    }

    void UploadData()
    {
        string path = Application.persistentDataPath + "datasave.json";
        if (File.Exists(path))
        {
            string jsonUpload = File.ReadAllText(path);
            settingsData = JsonUtility.FromJson<SettingsData>(jsonUpload);
        }
    }

    public void PlayButton()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    public void ChangeSkinButton(int number)
    {
        settingsData.arrayNumber = number;
    }

    IEnumerator LoadYourAsyncScene()
    {
        MainMenu.SetActive(false);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        LoadingScreen.SetActive(true);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void SettingsButton()
    {
        MainMenu.SetActive(false);
        Settings.SetActive(true);
    }

    public void BackToMenuButton()
    {
        Settings.SetActive(false);
        MainMenu.SetActive(true);
        string json = JsonUtility.ToJson(settingsData);
        File.WriteAllText(Application.persistentDataPath + "datasave.json", json);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}