using System;
using System.Collections;
using System.Collections.Generic;
using SBPScripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    public CharacterController player;
    public List<GameObject> _houseList;
    public List<GameObject> _shopList;
    public List<Shop_Interior_Manager> shopInteriorList;

    public Shop_Interior_Manager actualShopInterior;
    public GameObject actualShop;
    public GameObject actualHouse;

    public ShopProduct shopProduct;

    [Space, SerializeField]
    private Transform _contentSpawnMission;

    [SerializeField]
    private GameObject _prefabMission;
    
    [SerializeField]
    private float intervalAparition, maximumOrderList;

    [SerializeField]
    private List<GameObject> _listMission;
    [SerializeField]
    private List<OrdersSetInfo> _listMissionOrders;

    public GameObject CurrentMission, currentDisplayMission;
    [SerializeField]
    private GameObject _displayCurMissionContent, _displayCurMissionPrefab, _showPopapEnterExit;
    [SerializeField]
    private TextMeshProUGUI _distaceToTheHouse;

    [SerializeField] private Image displayRating;

    private int _stars = 5;
    private bool _isShop = false;
    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<CharacterController>();
        _showPopapEnterExit.SetActive(false);

        foreach (var t in _houseList)
        {
            t.SetActive(false);
        }
        Invoke(nameof(AddMission), 1.5f);
        // AddMission();
        StartCoroutine(MissionAddTimer());
    }

    private void Start()
    {
        CheckRating();
    }

    private void Update()
    {
        if (_listMission.Count > 0)
        {
            for (int i = 0; i < _listMissionOrders.Count; i++)
            {
                if (_listMissionOrders[i]._isTimerActive)
                {
                    if (_listMissionOrders[i]._timer < 0)
                    {
                        _listMissionOrders[i]._isTimerActive = false;
                        Destroy(_listMissionOrders[i].gameObject);
                        RemoveListUpdate();
                    }

                    _listMissionOrders[i]._timer -= Time.deltaTime;
                    _listMissionOrders[i]._imageTimer.fillAmount = _listMissionOrders[i]._timer / _listMissionOrders[i]._maxTimer;
                }
            }
        }

        // if (!_isShop && actualHouse != null)
        //     _distaceToTheHouse.text = Vector3.Distance(player.transform.position,actualHouse.transform.position).ToString();
    }

    IEnumerator MissionAddTimer()
    {
        yield return new WaitForSecondsRealtime(intervalAparition);
        if (maximumOrderList >= _listMission.Count)
        {
            AddMission();
            StartCoroutine(MissionAddTimer());
        }
    }

    public void EnterShop(ShopType shopType, GameObject Shop)
    {
        for (int i = 0; i < shopInteriorList.Count; i++)
        {
            if (shopInteriorList[i].shopType == shopType)
            {
                if(CurrentMission != null)
                    shopInteriorList[i].MissionCheck(CurrentMission.GetComponent<OrdersSetInfo>().shopTypeMission);
                else
                    shopInteriorList[i].MissionCheck(ShopType.None);

                actualShopInterior = shopInteriorList[i];
                player.transform.position = shopInteriorList[i].spawnEnter.position;
                actualShop = Shop;
                _isShop = true;
            }
        }
    }
    
    public void ExitShop()
    {
        player.transform.position = actualShop.transform.position;
        actualShopInterior = null;
        _isShop = false;
    }

    public void ShowPopapEnterExit(bool isActiv)
    {
        _showPopapEnterExit.SetActive(isActiv);
    }
    
    public void CommandStart()
    {
        if(actualShop.GetComponent<ShopMission>().shopType != CurrentMission.GetComponent<OrdersSetInfo>().shopTypeMission)
            return;
        
        if (_houseList.Count > 0)
            actualHouse = _houseList[Random.Range(0, _houseList.Count)].transform.gameObject;

        ShowPopapEnterExit(false);
        actualHouse.SetActive(true);
        //_distaceToTheHouse.gameObject.SetActive(true);
        float tempDistance = Vector3.Distance(actualShop.transform.position, actualHouse.transform.position);
        //_distaceToTheHouse.text = tempDistance.ToString();
        currentDisplayMission.GetComponent<OrdersSetInfo>().OrderHeatStatus(tempDistance / 1.5f);
    }

    public void CommandStop()
    {
        actualHouse.SetActive(false);
        actualHouse = null;

        MissionComplet(false);
    }

    private void AddMission()
    {
        if(_listMission.Count > 6)
            return;
        
        int tempIndex = Random.Range(1, 4);
        ShopMission tempShopMission = _shopList[Random.Range(0, _shopList.Count)].GetComponent<ShopMission>();
        GameObject tempMiss = Instantiate(_prefabMission, _contentSpawnMission);
        OrdersSetInfo tempOrdersSetInfo = tempMiss.GetComponent<OrdersSetInfo>();
                    tempOrdersSetInfo._maxTimer -= (10 * tempIndex); 
        tempOrdersSetInfo.OrdersSetInfoShop(tempShopMission.icoShop, tempShopMission.shopType.ToString(), tempShopMission.shopType);

        for (int i = 0; i <= tempIndex; i++)
        {
            for (int j = 0; j < shopProduct.shop.Count; j++)
            {
                if (tempShopMission.shopType == shopProduct.shop[j].ShopType)
                {
                    Product tempProduct = shopProduct.shop[j].listshop[Random.Range(0,shopProduct.shop[j].listshop.Count)];
                    tempOrdersSetInfo.OrdersSetInfoProduct(tempProduct.icon, tempProduct.productName, tempProduct.productPrice);
                    break;
                }
            }
        }
        _listMission.Add(tempMiss);
        _listMissionOrders.Add(tempOrdersSetInfo);
    }

    public void StartMission(GameObject mission)
    {
        if(CurrentMission != null)
            return;
        
        CurrentMission = mission;
        GameObject tempMiss = Instantiate(_displayCurMissionPrefab, _displayCurMissionContent.transform);
        OrdersSetInfo tempmissionOrdersSetInfo = mission.GetComponent<OrdersSetInfo>();
        OrdersSetInfo tempOrdersSetInfo = tempMiss.GetComponent<OrdersSetInfo>();
        tempOrdersSetInfo.OrdersSetInfoShop(tempmissionOrdersSetInfo._icoShop.sprite, tempmissionOrdersSetInfo._textNameShop.text, tempmissionOrdersSetInfo.shopTypeMission);
        tempOrdersSetInfo._price = tempmissionOrdersSetInfo._price;
        tempOrdersSetInfo.OrdersSetInfoProduct(tempmissionOrdersSetInfo._orders);

        currentDisplayMission = tempMiss;
        UIManager.instance.OpenClosePanel(UIManager.instance.missionPanel);
        if(actualShopInterior != null)
            actualShopInterior.MissionCheck(tempmissionOrdersSetInfo.shopTypeMission);
        CurrentMission.SetActive(false);
    }
    
    public void MissionComplet(bool isFail)
    {
        if (isFail)
        {
            Debug.LogError($"Mission Fail");
            SaveManager.instance.saveData.rating[0] += 1;
            SaveManager.instance.saveData.money -= currentDisplayMission.GetComponent<OrdersSetInfo>()._price / 2;
            
        }
        else
        {
            Debug.LogError($"Mission Compete -> {_stars} Stars");
            SaveManager.instance.saveData.rating[_stars] += 1;
            SaveManager.instance.saveData.money += currentDisplayMission.GetComponent<OrdersSetInfo>()._price;
        }
        
        //.gameObject.SetActive(false);
        Destroy(CurrentMission);
        Destroy(currentDisplayMission);
        RemoveListUpdate();
        StartCoroutine(MissionAddTimer());
        ShowPopapEnterExit(false);
        
        _stars = 5;
        CheckRating();
        SaveManager.instance.SaveData();
        UIManager.instance.UpdateMoneyText();
    }

    private void RemoveListUpdate()
    {
        _listMission.RemoveAll(x => x == null);
        _listMissionOrders.RemoveAll(x => x == null);
    }

    public void TimeOUT()
    {
        _stars--;

        switch (_stars)
        {
            case 4:
                currentDisplayMission.GetComponent<OrdersSetInfo>().heatStatus.color = Color.yellow;
                currentDisplayMission.GetComponent<OrdersSetInfo>().OrderHeatStatus(10f);
                break;
            case 3:
                currentDisplayMission.GetComponent<OrdersSetInfo>().heatStatus.color = Color.yellow;
                currentDisplayMission.GetComponent<OrdersSetInfo>().OrderHeatStatus(10f);
                break;
            case 2:
                currentDisplayMission.GetComponent<OrdersSetInfo>().heatStatus.color = Color.red;
                currentDisplayMission.GetComponent<OrdersSetInfo>().OrderHeatStatus(10f);
                break;
            case 1:
                currentDisplayMission.GetComponent<OrdersSetInfo>().heatStatus.color = Color.red;
                currentDisplayMission.GetComponent<OrdersSetInfo>().OrderHeatStatus(10f);
                break;
            case 0:
                MissionComplet(true);
                break;
        }
    }
    
    private void CheckRating()
    {
        int numRating = 0, numXmun = 0;
        for (int i = 0; i < SaveManager.instance.saveData.rating.Length; i++)
        {
            numRating += SaveManager.instance.saveData.rating[i];
            numXmun += (SaveManager.instance.saveData.rating[i] * i);
        }

        if (numRating != 0 && numXmun != 0)
        {
            float tempRating = numXmun / numRating;
            SaveManager.instance.saveData.averagRating = tempRating;
            displayRating.fillAmount = tempRating / 5;
        }
        else
        {
            displayRating.fillAmount = 0;
        }
    }
}
