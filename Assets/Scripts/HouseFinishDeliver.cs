using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBPScripts;

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
        MissionManager.instance.ShowPopapExitBike(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && MissionManager.instance.actualHouse != null)
        {
            if (other.gameObject.GetComponent<BicycleStatus>() != null)
            {
                if (other.gameObject.GetComponent<BicycleStatus>().onBike)
                {
                    MissionManager.instance.ShowPopapEnterExit(false);
                    MissionManager.instance.ShowPopapExitBike(true);
                    return;
                }
            }

            MissionManager.instance.ShowPopapExitBike(false);

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
                Debug.LogError($"<color=green> House Triger Activate </color>");
                MissionManager.instance.ShowPopapEnterExit(false);
                MissionManager.instance.CommandStop();
            }
        }
    }
}