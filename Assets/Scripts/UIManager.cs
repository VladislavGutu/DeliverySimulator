using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using nn.hid;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//using UnityEngine.Switch;
using UnityEngine.UI;
using Time = UnityEngine.Time;

namespace SBPScripts
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public Slider _staminaSlider;

        private BicycleController bicycleController;

        public Button missionBTN_Open, missionBTN_Close, selectBTmenu, tutorialBT;
        public GameObject missionPanel, missionContent, pausePanel, loadingPanel;

        public TextMeshProUGUI _textMoney;

        [Space,SerializeField]
        private DeliveryComplet _deliveryCompletPanel;
        [SerializeField]
        private Animator _deliveryStartAnimator;
        
        private bool _isPause = false;

        public GameObject _tutorialPanel;
        
        public bool IsPause
        {
            get => _isPause;
            set => _isPause = value;
        }
        
        void Start()
        {
            Debug.LogError("UIManager Start");

            if (PlayerPrefs.GetInt("FirstEnter") == 0)
            {
                _isPause = true;
                Time.timeScale = 0;
                _tutorialPanel.SetActive(true);
                EventSystem.current.SetSelectedGameObject(tutorialBT.gameObject);
            }
            
            instance = this;
            bicycleController = FindObjectOfType<BicycleController>();
            _staminaSlider.maxValue = StaminaManager.instance._staminaMaxAmount;

            missionBTN_Open.onClick.AddListener(() => { OpenClosePanel(missionPanel); });
            missionBTN_Close.onClick.AddListener(() => { OpenClosePanel(missionPanel); });
            
            UpdateMoneyText();
        }

        public void UpdateMoneyText()
        {
            SaveManager.instance.LoadData();
            _textMoney.text = SaveManager.instance.saveData.money.ToString();

        }
        // Update is called once per frame
        void Update()
        {
            if(_isPause)
                return;
#if UNITY_SWITCH
            if (NintendoInput.InputNpadButtonDown(NpadButton.Plus) || Input.GetKeyDown(KeyCode.Escape))
            {
                _isPause = true;
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                missionPanel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(selectBTmenu.gameObject);
            }
#endif
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


            _staminaSlider.value = StaminaManager.instance._staminaCurrentAmount;
#if UNITY_SWITCH
            if (NintendoInput.InputNpadButtonDown(NpadButton.Minus) || Input.GetKeyDown(KeyCode.Tab))
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
#endif
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
            
            #if UNITY_SWITCH
            NintendoInput.isActivInput = false;
            #endif
            
            loadingPanel.SetActive(true);
            DOVirtual.DelayedCall(2f, () =>
            {
                Time.timeScale = 1;
                _isPause = false;
                SceneManager.LoadScene("MainMenu");
            });

        }

        public void DeliveryStart()
        {
            _deliveryStartAnimator.SetTrigger("Open");
        }
        
        public void DeliveryComplet(int stars, int money)
        {
            _deliveryCompletPanel.SetDeliveryComplet(stars, money);
        }
        
        public void OpenClosePanel(GameObject panel)
        {
            panel.SetActive(!panel.activeInHierarchy);
        }
    }
}