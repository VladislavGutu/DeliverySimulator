using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public GameObject _shopPanel;
    public GameObject _mainPanel;
    public GameObject _player;
    public GameObject _bikeList;

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
        _bikeList.SetActive(true);
        _player.SetActive(false);
    }
    
    public void CloseShopPanel()
    {
        _shopPanel.SetActive(false);
        _bikeList.SetActive(false);
        _mainPanel.SetActive(true);
        _player.SetActive(true);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("TestSceneMission");
    }
}
