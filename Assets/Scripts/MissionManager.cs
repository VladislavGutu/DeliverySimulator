using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    public List<GameObject> _houseList;
    public List<GameObject> _shopList;

    public GameObject actualHouse;
    private void Awake()
    {
        instance = this;
    }

    public void CommandStart(ShopType shopType)
    {
        if (_houseList.Count > 0)
            actualHouse = _houseList[Random.Range(0, _houseList.Count)].transform.gameObject;
        
        actualHouse.SetActive(true);
        
        if (actualHouse != null)
        {
            foreach (var t in _shopList)
            {
                t.SetActive(false);
            }
        }
    }

    public void CommandStop()
    {
        actualHouse.SetActive(false);
        actualHouse = null;
        
        if (actualHouse == null)
        {
            foreach (var t in _shopList)
            {
                t.SetActive(true);
            }
        }
    }
}
