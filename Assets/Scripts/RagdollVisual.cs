using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollVisual : MonoBehaviour
{
    public List<Material> _materialList;
    public List<MeshRenderer> _bikeMaterial;
    
    public List<Material> _bodyMaterialList;
    public List<Material> _helmetMaterialList;
    
    public SkinnedMeshRenderer _bodyMesh;
    public SkinnedMeshRenderer _helmetMesh;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _bikeMaterial.Count; i++)
        {
            for (int k = 0; k < _bikeMaterial[i].materials.Length; k++)
            {
                if (_bikeMaterial[i].materials[k].name.Contains("BicycleBodyMetal"))
                {
                    _bikeMaterial[i].materials[k].color = _materialList[PlayerPrefs.GetInt("SelectedMaterial")].color;
                    _bikeMaterial[i].materials[k].mainTexture = _materialList[PlayerPrefs.GetInt("SelectedMaterial")].mainTexture;
                }
            }
        }
            
        for (int i = 0; i < _bodyMaterialList.Count; i++)
        {
            _bodyMesh.material.mainTexture = _bodyMaterialList[PlayerPrefs.GetInt("SelectedBodyMaterial",0)].mainTexture;
            _helmetMesh.materials[0].mainTexture = _helmetMaterialList[PlayerPrefs.GetInt("SelectedBodyMaterial", 0)].mainTexture;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
