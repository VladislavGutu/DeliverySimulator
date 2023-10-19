using System;
using System.Collections.Generic;
using UnityEngine;
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
        for (int i = 0; i < _bodyMaterials.Length; i++)
        {
            for (int k = 0; k < _bodyMaterials[i].materials.Length; k++)
            {
                if (_bodyMaterials[i].materials[k].name.Contains("BicycleBodyMetal"))
                {
                    _bodyMaterials[i].materials[k].color =
                        _materialList[PlayerPrefs.GetInt("SelectedMaterial", _selectedMaterial)].color;
                    _bodyMaterials[i].materials[k].mainTexture =
                        _materialList[PlayerPrefs.GetInt("SelectedMaterial", _selectedMaterial)].mainTexture;
                }
            }
        }

        ChangeBtnInShop();
    }

    private void Awake()
    {
        instance = this;
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
            PlayerPrefs.SetInt(_materialList[_currentMaterial].name, _currentMaterial);
            _selectedMaterial = _currentMaterial;
            SelectBike();
            ChangeBtnInShop();
        }
        else
        {
            Debug.LogError("Not Money");
        }
    }

    public void SelectBike()
    {
        PlayerPrefs.SetInt("SelectedMaterial", _selectedMaterial);
        MainMenu.instance._shopPanel.SetActive(false);
        MainMenu.instance._mainPanel.SetActive(true);
    }

    public int ChekMoney()
    {

        int _price = 300;

        return _price * _currentMaterial;
        
    }
    
    public void ChangeBtnInShop()
    {
        Debug.LogError($"material price ====> {_materialPrice} || currentmaterial ====> {_currentMaterial}");
        
        

        _materialPrice =ChekMoney();
        
        if (PlayerPrefs.GetInt(_materialList[_currentMaterial].name, 0) == _currentMaterial)
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
                    _bodyMaterials[i].materials[k].color = _materialList[PlayerPrefs.GetInt("SelectedMaterial")].color;
                    _bodyMaterials[i].materials[k].mainTexture = _materialList[PlayerPrefs.GetInt("SelectedMaterial")].mainTexture;
                }
            }
        }
    }
}