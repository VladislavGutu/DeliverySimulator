using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingSelector : MonoBehaviour
{
    [SerializeField] private GameObject _selectedObj;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_selectedObj);
    }
}
