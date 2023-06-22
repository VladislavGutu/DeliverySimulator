using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductSetInfo : MonoBehaviour
{
    public Image _ico;
    public TextMeshProUGUI _name, _price;

    public void ProductsSetInfo(Sprite icon, string name, int price)
    {
        _ico.sprite = icon;
        _name.text = name;
        
        if(_price != null)
            _price.text = price.ToString();
    }
    public void ProductsSetInfo(Sprite icon, string name)
    {
        _ico.sprite = icon;
        _name.text = name;
    }
}
