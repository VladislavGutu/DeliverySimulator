using System.Collections;
using System.Collections.Generic;
using nn.hid;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SBPScripts
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public Slider _staminaSlider;

        private BicycleController bicycleController;

        public Button missionBTN_Open, missionBTN_Close, selectBTmenu;
        public GameObject missionPanel, missionContent, pausePanel;

        private bool _isPause = false;
        void Start()
        {
            instance = this;
            bicycleController = FindObjectOfType<BicycleController>();
            _staminaSlider.maxValue = bicycleController._staminaMaxAmount;

            missionBTN_Open.onClick.AddListener(() => { OpenClosePanel(missionPanel); });
            missionBTN_Close.onClick.AddListener(() => { OpenClosePanel(missionPanel); });
        }

        // Update is called once per frame
        void Update()
        {
            if(_isPause)
                return;
            if (NintendoInput.InputNpadButtonDown(NpadButton.Plus))
            {
                _isPause = true;
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                EventSystem.current.SetSelectedGameObject(selectBTmenu.gameObject);
            }
                
            if (missionPanel.activeInHierarchy)
                if (EventSystem.current.currentSelectedGameObject == null)
                    if (missionContent.transform.childCount > 0)
                    {
                        if (missionContent.transform.GetChild(0).gameObject.GetComponent<OrdersSetInfo>()._ordersButton
                            .gameObject.activeInHierarchy)
                        {
                            EventSystem.current.SetSelectedGameObject(missionContent.transform.GetChild(0).gameObject
                                .GetComponent<OrdersSetInfo>()._ordersButton.gameObject);
                        }
                        else
                        {
                            EventSystem.current.SetSelectedGameObject(missionContent.transform.GetChild(1).gameObject
                                .GetComponent<OrdersSetInfo>()._ordersButton.gameObject);
                        }
                    }


            _staminaSlider.value = bicycleController._staminaCurrentAmount;
            if (NintendoInput.InputNpadButtonDown(NpadButton.Minus))
            {
                if (missionPanel.activeInHierarchy)
                {
                    missionPanel.SetActive(false);
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                {
                    missionPanel.SetActive(true);
                }
            }
        }

        public void Resume()
        {
            Time.timeScale = 1;
            _isPause = false;
            pausePanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
            _isPause = false;
            SceneManager.LoadScene("MainMenu");
        }
        
        public void OpenClosePanel(GameObject panel)
        {
            panel.SetActive(!panel.activeInHierarchy);
        }
    }
}