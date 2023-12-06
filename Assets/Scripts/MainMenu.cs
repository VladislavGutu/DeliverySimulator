using System;
using System.Collections;
using DG.Tweening;
using nn.hid;
using TMPro;
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
    public TextMeshProUGUI _textMoney;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_mainMenuButtonSelected);
    }

    private void Awake()
    {
        instance = this;
    }

    public void UpdateMoneyText()
    {
        _textMoney.text = SaveManager.instance.saveData.money.ToString();
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        BikeShop.instance.ChangeMaterials();
        PlayerVisual.instance.ChangeMaterialsAtStart();
        EventSystem.current.SetSelectedGameObject(_mainMenuButtonSelected);
        UpdateMoneyText();
    }

    private void Update()
    {
        // if (NintendoInput.InputNpadButtonDown(NpadButton.Minus) || Input.GetKeyDown(KeyCode.M))
        // {
        //     Debug.LogError("Plus Money");
        //     SaveManager.instance.saveData.money += 1000;
        //     SaveManager.instance.SaveData();
        //     UpdateMoneyText();
        // }
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
        
#if UNITY_SWITCH
        NintendoInput.isActivInput = false;
#endif
        
        DOVirtual.DelayedCall(2f, () => { SceneManager.LoadScene("TestSceneMission");});
    }
    
}