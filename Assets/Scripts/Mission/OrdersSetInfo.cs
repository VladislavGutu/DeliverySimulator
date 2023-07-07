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
     public Image _imageTimer;
     public float _maxTimer;
    public float _timer;
    
    public bool _isTimerActive = false;
    
    public int _price;
    public List<GameObject> _orders;

    public bool _isHeatTimerActive = false;
    public Image heatStatus;
    public float _heatMaxTimer;
    public float _heatTimer;
    private void Awake()
    {
        if(_ordersButton != null)
            _ordersButton.onClick.AddListener(() => { OrdersActivate();});
        if(heatStatus != null)
            heatStatus.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isHeatTimerActive)
        {
            if (_heatTimer < 0)
            {
                _isHeatTimerActive = false;
                MissionManager.instance.TimeOUT();
                //Destroy(this.gameObject);
            }
            
            _heatTimer -= Time.deltaTime;
            heatStatus.fillAmount = _heatTimer / _heatMaxTimer;
        }
    }

    private void OrdersActivate()
    {
        _isTimerActive = false;
        MissionManager.instance.StartMission(this.gameObject);
    }

    public void OrderHeatStatus(float timeHeat)
    {
        if (heatStatus != null)
        {
            _isHeatTimerActive = true;
            _heatMaxTimer = timeHeat;
            _heatTimer = timeHeat;
            heatStatus.transform.parent.gameObject.SetActive(true);
        }

    }
    
    public void OrdersSetInfoShop(Sprite icoShop, string nameShop, ShopType shopType)
    {
        _icoShop.sprite = icoShop;
        _textNameShop.text = nameShop;
        shopTypeMission = shopType;
        
        if (_imageTimer != null)
        {
            _isTimerActive = true;
            _timer = _maxTimer;
        }
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
