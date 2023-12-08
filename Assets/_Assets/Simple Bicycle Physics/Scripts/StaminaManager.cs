using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    public static StaminaManager instance;
    
    public float _staminaCurrentAmount;
    public float _staminaMaxAmount = 10;
    public bool sprint;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (sprint)
        {
            if (_staminaCurrentAmount > 0)
            {
                _staminaCurrentAmount -= Time.deltaTime;
            }
        }
        else
        {
            if (_staminaCurrentAmount < _staminaMaxAmount)
            {
                _staminaCurrentAmount += Time.deltaTime;
            }
            else
            {
                _staminaCurrentAmount = _staminaMaxAmount;
            }
        }

    }
}
