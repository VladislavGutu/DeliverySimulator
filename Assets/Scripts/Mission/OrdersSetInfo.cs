using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrdersSetInfo : MonoBehaviour
{
    public ShopType shopTypeMission;
    public Image _icoShop;
    public TextMeshProUGUI _textNameShop, _priceOrder;
    
    
    [SerializeField]
    private Transform _listProduct;
    [SerializeField]
    private GameObject _prefabProduct;

    [SerializeField] private Button _ordersButton;
    
    public int _price;
    public List<GameObject> _orders;

    
    private void Awake()
    {
        if(_ordersButton != null)
            _ordersButton.onClick.AddListener(()=>{MissionManager.instance.StartMission(this.gameObject);});
    }

    public void OrdersSetInfoShop(Sprite icoShop, string nameShop, ShopType shopType)
    {
        _icoShop.sprite = icoShop;
        _textNameShop.text = nameShop;
        shopTypeMission = shopType;
    }

    public void OrdersSetInfoProduct(Sprite icon, string name, int price)
    {
        GameObject tempObj = Instantiate(_prefabProduct, _listProduct);
        tempObj.GetComponent<ProductSetInfo>().ProductsSetInfo(icon, name, price);
        _orders.Add(tempObj);
        _price += price;
        _priceOrder.text = _price.ToString();
    }
    
    public void OrdersSetInfoProduct(List<GameObject> product)
    {
        for (int i = 0; i < product.Count; i++)
        {
            GameObject tempObj = Instantiate(_prefabProduct, _listProduct);
            tempObj.GetComponent<ProductSetInfo>().ProductsSetInfo(product[i].GetComponent<ProductSetInfo>()._ico.sprite, product[i].GetComponent<ProductSetInfo>()._name.text);
            _orders.Add(tempObj);
            _priceOrder.text = _price.ToString();
        }
    }
}
