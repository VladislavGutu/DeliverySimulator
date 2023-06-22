using System;
using System.Collections;
using System.Collections.Generic;
using SBPScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    public List<GameObject> _houseList;
    public List<GameObject> _shopList;

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

    public GameObject CurrentMission, currentDisplayMission;
    [SerializeField]
    private GameObject _displayCurMissionContent, _displayCurMissionPrefab;
    private void Awake()
    {
        instance = this;
        
        foreach (var t in _houseList)
        {
            t.SetActive(false);
        }
        
        Invoke(nameof(AddMission), 1.5f);
        // AddMission();
        StartCoroutine(MissionAddTimer());
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
    
    public void CommandStart(ShopType shopType)
    {
        if(shopType != CurrentMission.GetComponent<OrdersSetInfo>().shopTypeMission)
            return;
        
        if (_houseList.Count > 0)
            actualHouse = _houseList[Random.Range(0, _houseList.Count)].transform.gameObject;
        
        actualHouse.SetActive(true);
        
        // if (actualHouse != null)
        // {
        //     foreach (var t in _shopList)
        //     {
        //         t.SetActive(false);
        //     }
        // }
    }

    public void CommandStop()
    {
        actualHouse.SetActive(false);
        actualHouse = null;

        MissionComplet();
        // if (actualHouse == null)
        // {
        //     foreach (var t in _shopList)
        //     {
        //         t.SetActive(true);
        //     }
        // }
    }

    private void AddMission()
    {
        if(_listMission.Count > 6)
            return;
        
        ShopMission tempShopMission = _shopList[Random.Range(0, _shopList.Count)].GetComponent<ShopMission>();
        GameObject tempMiss = Instantiate(_prefabMission, _contentSpawnMission);
        OrdersSetInfo tempOrdersSetInfo = tempMiss.GetComponent<OrdersSetInfo>();
        tempOrdersSetInfo.OrdersSetInfoShop(tempShopMission.icoShop, tempShopMission.shopType.ToString(), tempShopMission.shopType);

        int tempIndex = Random.Range(1, 4);
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
        CurrentMission.SetActive(false);
    }
    
    public void MissionComplet()
    {
        Destroy(CurrentMission);
        Destroy(currentDisplayMission);
        _listMission.RemoveAll(x => x == null);
        StartCoroutine(MissionAddTimer());
    }
    
}
