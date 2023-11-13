using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerVisual : MonoBehaviour
{
    public static PlayerVisual instance;
    public List<Material> _bodyMaterialList;
    public List<Material> _helmetMaterialList;
        
    public SkinnedMeshRenderer _bodyMesh;
    public SkinnedMeshRenderer _helmetMesh;
    
    public int _currentMaterial;   
    public int _selectedMaterial;

    public GameObject _buyMaterialBtn;
    public GameObject _selectMaterialBtn;

    public Text _priceText;

    public int price = 300;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        ChangeMaterialsAtStart();
        EventSystem.current.SetSelectedGameObject(_buyMaterialBtn);
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeMaterialsAtStart();
    }

    public void NextBike()
    {
        if (_currentMaterial >= _bodyMaterialList.Count - 1)
            _currentMaterial = 0;
        else
            _currentMaterial++;

        ChangeMaterials();

        ChangeBtnInShop();
    }

    public void PreviousBike()
    {
        if (_currentMaterial <= 0)
            _currentMaterial = _bodyMaterialList.Count - 1;
        else
            _currentMaterial--;

        ChangeMaterials();

        ChangeBtnInShop();
    }

    public void ChangeMaterials()
    {
        for (int i = 0; i < _bodyMaterialList.Count; i++)
        { 
            _bodyMesh.material.mainTexture = _bodyMaterialList[_currentMaterial].mainTexture;
            _helmetMesh.materials[0].mainTexture = _helmetMaterialList[_currentMaterial].mainTexture;
        }
    }

    public void ChangeMaterialsAtStart()
    {
        for (int i = 0; i < _bodyMaterialList.Count; i++)
        { 
            _bodyMesh.material.mainTexture = _bodyMaterialList[PlayerPrefs.GetInt("SelectedBodyMaterial",0)].mainTexture;
            _helmetMesh.materials[0].mainTexture = _helmetMaterialList[PlayerPrefs.GetInt("SelectedBodyMaterial",0)].mainTexture;
        }
    }

    public void BuyBike()
    {
        if (SaveManager.instance.saveData.money >= price)
        {
             PlayerPrefs.SetInt(_bodyMaterialList[_currentMaterial].name, 1);
             SaveManager.instance.saveData.money -= price;
             SaveManager.instance.SaveData();
             SelectBike();
             ChangeBtnInShop();
             MainMenu.instance.UpdateMoneyText();

        }
        else
        {
            Debug.LogError("Not Money");
        }
    }
    
    public void SelectBike()
    {
        PlayerPrefs.SetInt(_bodyMaterialList[_currentMaterial].name, 1);
        PlayerPrefs.SetInt("SelectedBodyMaterial", _currentMaterial);
        MainMenu.instance._shopPanel.SetActive(false);
        MainMenu.instance._mainPanel.SetActive(true);
    }

    public int ChekMoney()
    {

        price = 300;

        return price * _currentMaterial;
        
    }
    
    public void ChangeBtnInShop()
    {
        price =ChekMoney();
        
        if (PlayerPrefs.GetInt(_bodyMaterialList[_currentMaterial].name, 0) == 1)
        {
            _buyMaterialBtn.SetActive(false);
            _selectMaterialBtn.SetActive(true);
        }
        else
        {
            _buyMaterialBtn.SetActive(true);
            _selectMaterialBtn.SetActive(false);
            _priceText.text = "$ " + price;
        }
    }

    public void RevertMaterial()
    {
        for (int i = 0; i < _bodyMaterialList.Count; i++)
        {
            _bodyMesh.material.mainTexture = _bodyMaterialList[PlayerPrefs.GetInt("SelectedBodyMaterial",0)].mainTexture;
            _helmetMesh.materials[0].mainTexture = _helmetMaterialList[PlayerPrefs.GetInt("SelectedBodyMaterial", 0)].mainTexture;

        }
    }
}
