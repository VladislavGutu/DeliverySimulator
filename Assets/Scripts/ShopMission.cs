using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMission : MonoBehaviour
{
    public ShopType shopType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && MissionManager.instance.actualHouse == null)
        {
            Debug.LogError($"<color=green> Shop Triger Activate </color>");
            MissionManager.instance.CommandStart(shopType);

        }
    }
}
