using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeShop : MonoBehaviour
{
    public int _currentBike;
    public int _selectedBike;
    
    public GameObject _buyBikeBtn;
    public GameObject _selectBikeBtn;
    
    public List<GameObject> _bikeList;

    private void OnEnable()
    {
        for (int i = 0; i < _bikeList.Count; i++)
        {
            _bikeList[i].SetActive(false);
        }
        _bikeList[_currentBike].SetActive(true);
    }

    public void NextBike()
    {
        if (_currentBike >= _bikeList.Count - 1)
            _currentBike = 0;
        else
            _currentBike++;
        
        for (int i = 0; i < _bikeList.Count; i++)
        {
            _bikeList[i].SetActive(false);
        }
        _bikeList[_currentBike].SetActive(true);
        ChangeBtnInShop();

    }

    public void PreviousBike()
    {
        if (_currentBike <= 0)
            _currentBike = _bikeList.Count - 1;
        else
            _currentBike--;
        
        for (int i = 0; i < _bikeList.Count; i++)
        {
            _bikeList[i].SetActive(false);
        }
        _bikeList[_currentBike].SetActive(true);
        ChangeBtnInShop();
    }

    public void BuyBike()
    {
        PlayerPrefs.SetInt(_bikeList[_currentBike].gameObject.name, _currentBike);
        ChangeBtnInShop();
    }

    public void SelectBike()
    {
        _selectedBike = _currentBike;
        PlayerPrefs.SetInt("SelectedBike", _selectedBike);
    }

    public void ChangeBtnInShop()
    {
        if (PlayerPrefs.GetInt(_bikeList[_currentBike].gameObject.name) == _currentBike)
        {
            _buyBikeBtn.SetActive(false);
            _selectBikeBtn.SetActive(true);
        }
    }
}
