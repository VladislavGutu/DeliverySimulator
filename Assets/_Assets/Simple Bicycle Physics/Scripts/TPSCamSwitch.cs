using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBPScripts
{
    public class TPSCamSwitch : MonoBehaviour
    {
        public GameObject cyclist;
        public GameObject externalCharacter;
        BicycleCamera bicycleCamera;
        BicycleStatus bicycleStatus;
        public PerfectMouseLook _TPSCameraControll;
        public PerfectMouseLook _FPSCameraControll;
        void Start()
        {
            bicycleCamera = FindObjectOfType<BicycleCamera>();
            bicycleStatus = FindObjectOfType<BicycleStatus>();
        }
        void LateUpdate()
        {
            if (externalCharacter != null)
            {
                if (externalCharacter.activeInHierarchy)
                {
                    bicycleCamera.target = externalCharacter.transform;
                }
                else
                {
                    bicycleCamera.target = cyclist.transform.root.transform;
                }
            }
            
            
            if (bicycleStatus.dislodged && bicycleStatus.instantiatedRagdoll!=null)
            {
                bicycleCamera.target = bicycleStatus.instantiatedRagdoll.transform.Find("mixamorig:Hips").gameObject.transform;
                _TPSCameraControll.enabled = true;
                _FPSCameraControll.enabled = false;
            }
            else if(externalCharacter==null)
            {
                bicycleCamera.target = cyclist.transform.root.transform;
                _TPSCameraControll.enabled = false;
                _FPSCameraControll.enabled = true;
            }
        }
    }
}
