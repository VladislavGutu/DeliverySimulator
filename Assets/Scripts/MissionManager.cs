using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    public List<GameObject> houseList;

    public GameObject actualHouse;
    private void Awake()
    {
        instance = this;
    }

    public void CommandStart(ShopType shopType)
    {
        if (houseList.Count > 0)
        actualHouse = houseList[0].transform.gameObject;
        actualHouse.SetActive(true);
    }

    public void CommandStop()
    {
        actualHouse.SetActive(false);
        actualHouse = null;
    }
}
