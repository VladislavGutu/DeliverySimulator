using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFinishDeliver : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && MissionManager.instance.actualHouse != null)
        {
            // MissionManager.instance.CommandStart(shopType);
            MissionManager.instance.ShowPopapEnterExit(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && MissionManager.instance.actualHouse != null)
            MissionManager.instance.ShowPopapEnterExit(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && MissionManager.instance.actualHouse != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.LogError($"<color=green> House Triger Activate </color>");
                MissionManager.instance.ShowPopapEnterExit(false);
                MissionManager.instance.CommandStop();
            }
        }
    }
    
}
