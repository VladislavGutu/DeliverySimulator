using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Shop_Interior : MonoBehaviour
{
    [SerializeField]
    private GameObject _exit;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && _exit != null && !_exit.activeInHierarchy)
        {
            _exit.SetActive(true);
        }
        else if (other.gameObject.tag.Contains("Player") && _exit != null && _exit.activeInHierarchy)
        {
            MissionManager.instance.ShowPopapEnterExit(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && _exit != null && _exit.activeInHierarchy)
        {
            MissionManager.instance.ShowPopapEnterExit(true);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && _exit != null && _exit.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MissionManager.instance.ExitShop();
            }
        }
    }

}
