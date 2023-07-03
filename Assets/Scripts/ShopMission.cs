using System;
using System.Collections;
using System.Collections.Generic;
using SickscoreGames.HUDNavigationSystem;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopMission : MonoBehaviour
{
    public HUDNavigationElement _hudNavigationElement;
    
    public ShopType shopType;
    public List<Sprite> _shopSprite;

    public Sprite icoShop;
    
    private void Start()
    {
        Invoke(nameof(SetShopIcon), .5f);
    }

    public void SetShopIcon()
    {
        switch (shopType)
        {
            case ShopType.Pizza:
                _hudNavigationElement.Indicator.OffscreenIcon.sprite = _shopSprite[0];
                _hudNavigationElement.Indicator.OnscreenIcon.sprite = _shopSprite[0];
                icoShop = _shopSprite[0];
                break;
            case ShopType.MC:
                _hudNavigationElement.Indicator.OffscreenIcon.sprite = _shopSprite[1];
                _hudNavigationElement.Indicator.OnscreenIcon.sprite = _shopSprite[1];
                icoShop = _shopSprite[1];
                break;
            case ShopType.KFC:
                _hudNavigationElement.Indicator.OffscreenIcon.sprite = _shopSprite[2];
                _hudNavigationElement.Indicator.OnscreenIcon.sprite = _shopSprite[2];
                icoShop = _shopSprite[2];
                break;
            case ShopType.Sushi:
                _hudNavigationElement.Indicator.OffscreenIcon.sprite = _shopSprite[3];
                _hudNavigationElement.Indicator.OnscreenIcon.sprite = _shopSprite[3];
                icoShop = _shopSprite[3];
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            // MissionManager.instance.CommandStart(shopType);
            MissionManager.instance.ShowPopapEnterExit(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
            MissionManager.instance.ShowPopapEnterExit(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.LogError($"<color=green> Shop Trigger Activate </color>");
               MissionManager.instance.EnterShop(shopType, this.gameObject);
            }
        }
    }
}
