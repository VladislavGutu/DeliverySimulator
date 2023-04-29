using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SBPScripts
{
    public class UIManager : MonoBehaviour
    {
        public Slider _staminaSlider;

        private BicycleController bicycleController;

        void Start()
        {
            bicycleController = FindObjectOfType<BicycleController>();
            _staminaSlider.maxValue = bicycleController._staminaMaxAmount;

        }

        // Update is called once per frame
        void Update()
        {
            _staminaSlider.value = bicycleController._staminaCurrentAmount;
        }
    }
}