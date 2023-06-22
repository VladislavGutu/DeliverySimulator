using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SBPScripts
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public Slider _staminaSlider;

        private BicycleController bicycleController;

        public Button missionBTN_Open, missionBTN_Close;
        public GameObject missionPanel;
        
        
        void Start()
        {
            instance = this;
            bicycleController = FindObjectOfType<BicycleController>();
            _staminaSlider.maxValue = bicycleController._staminaMaxAmount;
            
            missionBTN_Open.onClick.AddListener(()=>{OpenClosePanel(missionPanel);});
            missionBTN_Close.onClick.AddListener(()=>{OpenClosePanel(missionPanel);});

        }

        // Update is called once per frame
        void Update()
        {
            _staminaSlider.value = bicycleController._staminaCurrentAmount;
        }

        public void OpenClosePanel(GameObject panel)
        {
            panel.SetActive(!panel.activeInHierarchy);
        }
    }
}