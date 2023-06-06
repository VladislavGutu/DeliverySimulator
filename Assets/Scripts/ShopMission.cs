using System;
using System.Collections;
using System.Collections.Generic;
using SickscoreGames.HUDNavigationSystem;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopMission : MonoBehaviour
{
    public HUDNavigationElement _hudNavigationElement;
    
    public ShopType shopType;
    public List<Sprite> _shopSprite;

    private void Start()
    {
        Invoke(nameof(SetShopIcon), .5f);
    }

    private void SetShopIcon()
    {
        switch (shopType)
        {
            case ShopType.Pizza:
                _hudNavigationElement.CompassBar.Icon.sprite = _shopSprite[0];
                break;
            case ShopType.MC:
                _hudNavigationElement.CompassBar.Icon.sprite = _shopSprite[1];
                break;
            case ShopType.KFC:
                _hudNavigationElement.CompassBar.Icon.sprite = _shopSprite[2];
                break;
            case ShopType.Sushi:
                _hudNavigationElement.CompassBar.Icon.sprite = _shopSprite[3];
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && MissionManager.instance.actualHouse == null)
        {
            Debug.LogError($"<color=green> Shop Triger Activate </color>");
            MissionManager.instance.CommandStart(shopType);

        }
    }
}
