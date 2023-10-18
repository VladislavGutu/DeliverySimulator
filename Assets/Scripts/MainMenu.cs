using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public GameObject _shopPanel;
    public GameObject _mainPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenShopPanel()
    {
        _shopPanel.SetActive(true);
        _mainPanel.SetActive(false);
    }
    
    public void CloseShopPanel()
    {
        _shopPanel.SetActive(false);
        _mainPanel.SetActive(true);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("TestSceneMission");
    }
}
