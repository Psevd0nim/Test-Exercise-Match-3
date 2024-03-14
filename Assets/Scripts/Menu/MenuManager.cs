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
    public SettingsData settingsData;
    public GameObject[] checkMarks;
    public GameObject Modes;
    public GameObject Levels;
    //private DataPersistence dataPersistence = DataPersistence.Instance;

    private void Awake()
    {
        UploadData();
    }

    void UploadData()
    {
        string path = Application.persistentDataPath + "/datasave.json";
        if (File.Exists(path))
        {
            string jsonUpload = File.ReadAllText(path);
            settingsData = JsonUtility.FromJson<SettingsData>(jsonUpload);
        }
        checkMarks[settingsData.arrayNumber].SetActive(true);
    }

    public void PlayButton()
    {
        MainMenu.SetActive(false);
        Modes.SetActive(true);
    }
    
    public void EndlessModeButton()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    public void LevelsModeButton()
    {
        Modes.SetActive(false);
        Levels.SetActive(true);
    }

    public void ChangeSkinButton(int number)
    {
        checkMarks[settingsData.arrayNumber].SetActive(false);
        settingsData.arrayNumber = number;
        checkMarks[settingsData.arrayNumber].SetActive(true);
    }

    IEnumerator LoadYourAsyncScene()
    {
        Modes.SetActive(false);
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

    public void BackToMenuButtonFromSettings()
    {
        Settings.SetActive(false);
        MainMenu.SetActive(true);
        string json = JsonUtility.ToJson(settingsData);
        File.WriteAllText(Application.persistentDataPath + "/datasave.json", json);
        UpdateData();
    }

    public void BackToMenuButton()
    {
        MainMenu.SetActive(true);
        Modes.SetActive(false);
    }

    public void BackToModesButton()
    {
        Modes.SetActive(true);
        Levels.SetActive(false);
    }

    void UpdateData()
    {
        DataPersistence.Instance.arrayNumber = settingsData.arrayNumber;
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