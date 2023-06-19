using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUpRestaurant : MonoBehaviour
{
    public MeshRenderer _mainMesh;
    public GameObject _pointMaterial;
    public List<MeshRenderer> _restaurantMeshList;
    
    private ShopMission _shopMission;
    
    // Start is called before the first frame update
    void Start()
    {
        _shopMission = GetComponent<ShopMission>();
        
        switch (_shopMission.shopType)
        {
            case ShopType.Pizza:
                _mainMesh.material.color = Color.red;
                _pointMaterial.GetComponent<MeshRenderer>().material.color = _mainMesh.material.color;
                Debug.LogError("Pizza SettingUpRestaurant");
                break;
            case ShopType.MC:
                _mainMesh.material.color = Color.green;
                _pointMaterial.GetComponent<MeshRenderer>().material.color = _mainMesh.material.color;
                Debug.LogError("MC SettingUpRestaurant");
                break;
            case ShopType.KFC:
                _mainMesh.material.color = Color.magenta;
                _pointMaterial.GetComponent<MeshRenderer>().material.color = _mainMesh.material.color;
                Debug.LogError("KFC SettingUpRestaurant");
                break;
            case ShopType.Sushi:
                _mainMesh.material.color = Color.cyan;
                _pointMaterial.GetComponent<MeshRenderer>().material.color = _mainMesh.material.color;
                Debug.LogError("Sushi SettingUpRestaurant");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && MissionManager.instance.actualHouse == null)
        {
            switch (_shopMission.shopType)
            {
                case ShopType.Pizza:
                    Debug.LogError("Pizza Scene");
                    SceneManager.LoadScene("Pizza");
                    break;
                case ShopType.MC:
                    Debug.LogError("MC Scene");
                    SceneManager.LoadScene("MC");
                    break;
                case ShopType.KFC:
                    Debug.LogError("KFC Scene");
                    SceneManager.LoadScene("KFC");
                    break;
                case ShopType.Sushi:
                    Debug.LogError("Sushi Scene");
                    SceneManager.LoadScene("Sushi");
                    break;
            }
        }
    }
}
