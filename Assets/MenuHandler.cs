using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject mapSelectPanel;

    public static MenuHandler main;
    private string currentMap = "Field";

    private void Awake()
    {
        main = this;
        SetPanel("Menu");   
    }

    public void SetPanel(string name)
    {
        if (name == "Menu")
        {
            DisableAll();
            menuPanel.SetActive(true);
        }
        else if (name == "Map")
        {
            DisableAll();
            mapSelectPanel.SetActive(true);
        }
        else print(name + "이 존재하지 않음");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetMap(string name)
    {
        currentMap = name;
    }

    public void StartMap()
    {
        if (SceneManager.GetSceneByName(currentMap) != null)
        {
            SceneManager.LoadScene(currentMap);
        }
    }

    private void DisableAll()
    {
        menuPanel.SetActive(false);
        mapSelectPanel.SetActive(false);
    }
}
