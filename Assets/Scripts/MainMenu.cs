using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public GameObject _shopPanel;
    public GameObject _mainPanel;
    public GameObject _shopButtonSelected;
    public GameObject _mainMenuButtonSelected;
    public GameObject _loadingPanel;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_mainMenuButtonSelected);

    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        BikeShop.instance.ChangeMaterials();
        PlayerVisual.instance.ChangeMaterialsAtStart();
    }

    public void OpenShopPanel()
    {
        _shopPanel.SetActive(true);
        _mainPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_shopButtonSelected);
    }
    
    public void CloseShopPanel()
    {
        _shopPanel.SetActive(false);
        _mainPanel.SetActive(true);
        BikeShop.instance.RevertMaterial();

        EventSystem.current.SetSelectedGameObject(_mainMenuButtonSelected);
    }
    
    public void StartGame()
    {
        _loadingPanel.SetActive(true);
        DOVirtual.DelayedCall(2f, () =>
        {
            SceneManager.LoadScene("TestSceneMission");
        });
    }
}
