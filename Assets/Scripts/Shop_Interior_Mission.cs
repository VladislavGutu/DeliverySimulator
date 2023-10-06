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
#if UNITY_SWITCH
            bool isEnterExit;
            if (NintendoInput.isEditorInputActiv)
                isEnterExit = NintendoInput.InputNpadButtonDown(nn.hid.NpadButton.A);
            else
                isEnterExit = Input.GetKeyDown(KeyCode.E);

            if (isEnterExit)
#else
            if (Input.GetKeyDown(KeyCode.E))
#endif
            {
                MissionManager.instance.CommandStart();
                this.gameObject.SetActive(false);
            }
        }
    }
}