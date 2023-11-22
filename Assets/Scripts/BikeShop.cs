using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BikeShop : MonoBehaviour
{
    public static BikeShop instance;
    
    public int _currentMaterial;
    public int _selectedMaterial;

    public GameObject _buyMaterialBtn;
    public GameObject _selectMaterialBtn;

    public Text _priceText;

    public int _materialPrice = 300;

    public List<Material> _materialList;
    
    public MeshRenderer[] _bodyMaterials;

    private void OnEnable()
    {
        LoadBikeMaterial();

        ChangeBtnInShop();
    }

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        LoadBikeMaterial();
        ChangeBtnInShop();
    }

    public void NextBike()
    {
        if (_currentMaterial >= _materialList.Count - 1)
            _currentMaterial = 0;
        else
            _currentMaterial++;

        ChangeMaterials();

        ChangeBtnInShop();
    }

    public void PreviousBike()
    {
        if (_currentMaterial <= 0)
            _currentMaterial = _materialList.Count - 1;
        else
            _currentMaterial--;

        ChangeMaterials();

        ChangeBtnInShop();
    }

    public void ChangeMaterials()
    {
        for (int i = 0; i < _bodyMaterials.Length; i++)
        {
            for (int k = 0; k < _bodyMaterials[i].materials.Length; k++)
            {
                if (_bodyMaterials[i].materials[k].name.Contains("BicycleBodyMetal"))
                {
                    _bodyMaterials[i].materials[k].color = _materialList[_currentMaterial].color;
                    _bodyMaterials[i].materials[k].mainTexture = _materialList[_currentMaterial].mainTexture;
                }
            }
        }
    }

    public void BuyBike()
    {
        if (SaveManager.instance.saveData.money >= ChekMoney())
        {
            PlayerPrefs.SetInt(_materialList[_currentMaterial].name, 1);
            SaveManager.instance.saveData.money -= _materialPrice;

            SelectBike();
            ChangeBtnInShop();
            SaveManager.instance.SaveData();

            MainMenu.instance.UpdateMoneyText();
        }
        else
        {
            NotifyManager.Instance.ShowMessageBox("", "YOU DON'T HAVE ENOUGH MONEY!");

        }
    }

    public void SelectBike()
    {
        PlayerPrefs.SetInt(_materialList[_currentMaterial].name, 1);
        PlayerPrefs.SetInt("SelectedMaterial", _currentMaterial);
        MainMenu.instance._shopPanel.SetActive(false);
        MainMenu.instance._mainPanel.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(MainMenu.instance._mainMenuButtonSelected);
    }
    
    public int ChekMoney()
    {

        int _materialPrice = 300;

        return _materialPrice * _currentMaterial;
        
    }

    public void LoadBikeMaterial()
    {
        for (int i = 0; i < _bodyMaterials.Length; i++)
        {
            for (int k = 0; k < _bodyMaterials[i].materials.Length; k++)
            {
                if (_bodyMaterials[i].materials[k].name.Contains("BicycleBodyMetal"))
                {
                    _bodyMaterials[i].materials[k].color =
                        _materialList[PlayerPrefs.GetInt("SelectedMaterial" ,0)].color;
                    _bodyMaterials[i].materials[k].mainTexture =
                        _materialList[PlayerPrefs.GetInt("SelectedMaterial", 0)].mainTexture;
                }
            }
        }
    }
    
    public void ChangeBtnInShop()
    {
        Debug.LogError($"material price ====> {_materialPrice} || currentmaterial ====> {_currentMaterial}");
        
        _materialPrice = ChekMoney();
        
        if (PlayerPrefs.GetInt(_materialList[_currentMaterial].name, 0) == 1)
        {
            _buyMaterialBtn.SetActive(false);
            _selectMaterialBtn.SetActive(true);
        }
        else
        {
            _buyMaterialBtn.SetActive(true);
            _selectMaterialBtn.SetActive(false);
            _priceText.text = "$ " + _materialPrice;
        }
    }

    public void RevertMaterial()
    {
        for (int i = 0; i < _bodyMaterials.Length; i++)
        {
            for (int k = 0; k < _bodyMaterials[i].materials.Length; k++)
            {
                if (_bodyMaterials[i].materials[k].name.Contains("BicycleBodyMetal"))
                {
                    _bodyMaterials[i].materials[k].color = _materialList[PlayerPrefs.GetInt("SelectedMaterial",0)].color;
                    _bodyMaterials[i].materials[k].mainTexture = _materialList[PlayerPrefs.GetInt("SelectedMaterial",0)].mainTexture;
                }
            }
        }
    }
}