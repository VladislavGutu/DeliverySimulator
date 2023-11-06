using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image _loadingFill;
    
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _loadingFill.fillAmount += Time.deltaTime * .6f;
    }
}
