using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace SBPScripts
{
    public class BicycleCamera : MonoBehaviour
    {

        public Transform target;
        public bool stuntCamera;
        public float distance = 20.0f;
        public float height = 5.0f;
        public float heightDamping = 2.0f;

        public float lookAtHeight = 0.0f;

        public float rotationSnapTime = 0.3F;


        private Vector3 lookAtVector;

        private float usedDistance;

        float wantedRotationAngle;
        float wantedHeight;

        float currentRotationAngle;
        float currentHeight;

        Vector3 wantedPosition;

        private float yVelocity = 0.0F;
        private float zVelocity = 0.0F;
        PerfectMouseLook perfectMouseLook;

        private PhotonView _photonView;


        void Start()
        {
            perfectMouseLook = GetComponent<PerfectMouseLook>();
            
            // AllNeededReferences[] tempANR = FindObjectsOfType<AllNeededReferences>();
            //             
            //             for (int i = 0; i < tempANR.Length; i++)
            //             {
            //                 if (tempANR[i]._PhotonView.IsMine)
            //                 {
            //                     target = tempANR[i]._BicycleController.gameObject.transform;
            //                     _photonView = tempANR[i]._PhotonView;
            //                 }
            //             }
            //             Debug.LogError(tempANR.Length);
            
            target = GameObject.FindObjectOfType<BicycleController>().transform;
            if (stuntCamera)
            {
                var follow = new GameObject("Follow");
                var toFollow = GameObject.FindObjectOfType<BicycleController>().transform;
                follow.transform.SetParent(toFollow);
                follow.transform.position = toFollow.position + toFollow.gameObject.GetComponent<BoxCollider>().center;
                target = follow.transform;

                height -= toFollow.gameObject.GetComponent<BoxCollider>().center.y;
                lookAtHeight -= toFollow.gameObject.GetComponent<BoxCollider>().center.y;
            }
            else
            {
            }
        }

        void LateUpdate()
        {
            // if (_photonView.IsMine)
            // {
                if (target != null)
                {
                    wantedHeight = target.position.y + height;
                    currentHeight = transform.position.y;
                    wantedRotationAngle = target.eulerAngles.y;
                    currentRotationAngle = transform.eulerAngles.y;
                    if (perfectMouseLook.movement == false)
                        currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref yVelocity, rotationSnapTime);

                    currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.fixedDeltaTime);

                    wantedPosition = target.position;
                    wantedPosition.y = currentHeight;

                    usedDistance = Mathf.SmoothDampAngle(usedDistance, distance, ref zVelocity, 0.1f);

                    wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);

                    transform.position = wantedPosition;

                    transform.LookAt(target.position + lookAtVector);

                    lookAtVector = new Vector3(0, lookAtHeight, 0);
                }
            // }
        }
    }
}