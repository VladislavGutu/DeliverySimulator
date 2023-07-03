using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Interior_Mission : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            MissionManager.instance.ShowPopapEnterExit(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            MissionManager.instance.ShowPopapEnterExit(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MissionManager.instance.CommandStart();
                this.gameObject.SetActive(false);
            }
        }

    }
}
