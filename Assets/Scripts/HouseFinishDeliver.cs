using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFinishDeliver : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && MissionManager.instance.actualHouse != null)
        {
            Debug.LogError($"<color=green> House Triger Activate </color>");
            MissionManager.instance.CommandStop();

        }

    }
}
