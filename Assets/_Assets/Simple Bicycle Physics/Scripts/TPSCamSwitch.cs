using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace SBPScripts
{
    public class TPSCamSwitch : MonoBehaviour
    {
        public GameObject cyclist;
        public GameObject externalCharacter;
        public BicycleCamera bicycleCamera;
        public BicycleStatus bicycleStatus;
        public PerfectMouseLook _TPSCameraControll;
        public PerfectMouseLook _FPSCameraControll;

        private PhotonView _photonView;
        void Start()
        {
            _photonView = GetComponent<PhotonView>();

            AllNeededReferences[] tempANR = FindObjectsOfType<AllNeededReferences>();

            bicycleCamera = GetComponent<BicycleCamera>();
            
            for (int i = 0; i < tempANR.Length; i++)
            {
                if (tempANR[i]._PhotonView.IsMine)
                {
                    cyclist = tempANR[i]._BicycleController.gameObject;
                    bicycleStatus = tempANR[i]._BicycleStatus;

                    externalCharacter = tempANR[i]._ExternalCharacter;
                    _FPSCameraControll = tempANR[i]._PerfectMouseLook;
                    _photonView = tempANR[i]._PhotonView;
                }
            }
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

                if (bicycleStatus.dislodged && bicycleStatus.instantiatedRagdoll != null)
                {
                    bicycleCamera.target = bicycleStatus.instantiatedRagdoll.transform.Find("mixamorig:Hips").gameObject
                        .transform;
                    _TPSCameraControll.enabled = true;
                    _FPSCameraControll.enabled = false;
                }
                else if (externalCharacter == null)
                {
                    bicycleCamera.target = cyclist.transform.root.transform;
                    _TPSCameraControll.enabled = false;
                    _FPSCameraControll.enabled = true;
                }
        }
    }
}
