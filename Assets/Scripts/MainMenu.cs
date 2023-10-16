using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject _shopPanel;
    public GameObject _mainPanel;
    public GameObject _player;
    
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OpenShopPanel()
    {
        _shopPanel.SetActive(true);
        _mainPanel.SetActive(false);
        _player.SetActive(false);
    }
    
    public void CloseShopPanel()
    {
        _shopPanel.SetActive(false);
        _mainPanel.SetActive(true);
        _player.SetActive(true);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("TestSceneMission");
    }
}
