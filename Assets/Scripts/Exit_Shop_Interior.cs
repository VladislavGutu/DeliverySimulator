using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_SWITCH
using nn.hid;
#endif
using UnityEngine;

public class Exit_Shop_Interior : MonoBehaviour
{
    [SerializeField] private GameObject _exit;

    private bool _isPlayerInTrigger;


//     private void Update()
//     {
//         if (!_isPlayerInTrigger)
//             return;
//         
// #if UNITY_SWITCH
//         bool isEnterExit;
//         if (NintendoInput.isEditorInputActiv)
//             isEnterExit = NintendoInput.InputNpadButtonDown(NpadButton.A);
//         else
//             isEnterExit = Input.GetKeyDown(KeyCode.E);
//
//         if (isEnterExit)
// #else
//             if (Input.GetKeyDown(KeyCode.E))
// #endif
//         {
//             MissionManager.instance.ExitShop();
//         }
//
//     }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && _exit != null && !_exit.activeInHierarchy)
        {
            _exit.SetActive(true);
        }
        else if (other.gameObject.tag.Contains("Player") && _exit != null && _exit.activeInHierarchy)
        {
            MissionManager.instance.ShowPopapEnterExit(false);
            _isPlayerInTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && _exit != null && _exit.activeInHierarchy)
        {
            MissionManager.instance.ShowPopapEnterExit(true);
            _isPlayerInTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && _exit != null && _exit.activeInHierarchy)
        {
#if UNITY_SWITCH
            bool isEnterExit;
            if (NintendoInput.isEditorInputActiv)
                isEnterExit = NintendoInput.InputNpadButtonDown(NpadButton.A);
            else
                isEnterExit = Input.GetKeyDown(KeyCode.E);

            if (isEnterExit)
#else
            if (Input.GetKeyDown(KeyCode.E))
#endif
            {
                MissionManager.instance.ExitShop();
            }
        }
    }
}