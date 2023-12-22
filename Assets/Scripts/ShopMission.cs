using System;
using System.Collections;
using System.Collections.Generic;
using SBPScripts;
#if UNITY_SWITCH
using nn.hid;
#endif
using SickscoreGames.HUDNavigationSystem;
using Unity.VisualScripting;
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

    private bool _isPlayerInTrigger; 
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
            case ShopType.KTS:
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

    void Update()
    {
        if (!_isPlayerInTrigger)
            return;

#if UNITY_SWITCH
        bool isEnterExit;
        if (NintendoInput.isEditorInputActiv)
            isEnterExit = NintendoInput.InputNpadButtonDown(NpadButton.A);
        else
            isEnterExit = Input.GetKeyDown(KeyCode.E);

        if (isEnterExit)
#else
            if (Input.GetKeyDown(KeyCode.E))
#endif
        {
            Debug.LogError($"<color=green> Shop Trigger Activate </color>");
            MissionManager.instance.EnterShop(shopType, this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            // MissionManager.instance.CommandStart(shopType);
            MissionManager.instance.ShowPopapEnterExit(true);
            _isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            MissionManager.instance.ShowPopapEnterExit(false);
            _isPlayerInTrigger = false;
        }
        MissionManager.instance.ShowPopapExitBike(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            if (other.gameObject.GetComponent<BicycleStatus>() != null)
            {
                if (other.gameObject.GetComponent<BicycleStatus>().onBike)
                {
                    MissionManager.instance.ShowPopapEnterExit(false);
                    MissionManager.instance.ShowPopapExitBike(true);
                    return;
                }
            }

            MissionManager.instance.ShowPopapExitBike(false);
        }
    }
}