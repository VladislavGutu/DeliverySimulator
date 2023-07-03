using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Interior_Manager : MonoBehaviour
{

    public ShopType shopType;
    public Transform spawnEnter;
    public GameObject missionTake;

    public void MissionCheck(ShopType type)
    {
        Debug.LogError($"{shopType} == {type}");
        if (type != null && shopType == type && MissionManager.instance.actualHouse == null)
            missionTake.SetActive(true);
        else
            missionTake.SetActive(false);
    }
}
