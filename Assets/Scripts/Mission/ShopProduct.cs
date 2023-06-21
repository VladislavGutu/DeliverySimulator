using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShopProduct", menuName = "ScriptableObjects/ShopProduct")]
public class ShopProduct : ScriptableObject
{
    public List<Shop> shop = new List<Shop>();
}

[System.Serializable]
public class Shop
{
    public ShopType ShopType;
    public Sprite icon;
    public List<Product> listshop;
}

[System.Serializable]
public class Product
{
    public string productName;
    public int productPrice;
    public Sprite icon;
}
