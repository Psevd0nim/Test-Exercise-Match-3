using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence Instance { get; private set; }
    private MenuManager menuManager;
    public int arrayNumber;

    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        menuManager = GameObject.Find("Canvas").GetComponent<MenuManager>();
        arrayNumber = menuManager.settingsData.arrayNumber;
    }

    public void UpdateData()
    {
        arrayNumber = menuManager.settingsData.arrayNumber;
    }


}