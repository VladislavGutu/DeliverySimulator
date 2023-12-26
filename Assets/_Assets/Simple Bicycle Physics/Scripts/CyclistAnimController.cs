using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_SWITCH
using nn.hid;
#endif
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace SBPScripts
{
    public class CyclistAnimController : MonoBehaviour
    {
        public List<Material> _materialList;
        public List<MeshRenderer> _bikeMaterial;

        BicycleController bicycleController;
        Animator anim;
        string clipInfoCurrent, clipInfoLast;
        [HideInInspector] public float speed;
        [HideInInspector] public bool isAirborne;

        public GameObject hipIK, chestIK, leftFootIK, leftFootIdleIK, headIK;
        
        BicycleStatus bicycleStatus;
        Rig rig;
        bool onOffBike;

        [Header("Character Switching")] [Space]
        public GameObject cyclist;

        public GameObject externalCharacter;
        float waitTime, prevLocalPosX;
        
        public List<Material> _bodyMaterialList;
        public List<Material> _helmetMaterialList;
        
        public SkinnedMeshRenderer _bodyMesh;
        public SkinnedMeshRenderer _helmetMesh;
        
        public bool _isNearBicycle;
        public bool _isEnterBicycle = true;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _isEnterBicycle = true;
            }
            if (other.CompareTag("Bike"))
            {
                _isNearBicycle = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _isEnterBicycle = false;
            }
            if (other.CompareTag("Bike"))
            {
                _isNearBicycle = false;
            }
        }

        void Start()
        {
            _isEnterBicycle = true;
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
            
            bicycleController = FindObjectOfType<BicycleController>();
            bicycleStatus = FindObjectOfType<BicycleStatus>();
            rig = hipIK.transform.parent.gameObject.GetComponent<Rig>();
            if (bicycleStatus != null)
                onOffBike = bicycleStatus.onBike;
            if (cyclist != null)
                cyclist.SetActive(bicycleStatus.onBike);
            if (externalCharacter != null)
                externalCharacter.SetActive(!bicycleStatus.onBike);
            anim = GetComponent<Animator>();
            leftFootIK.GetComponent<TwoBoneIKConstraint>().weight = 0;
            chestIK.GetComponent<TwoBoneIKConstraint>().weight = 0;
            hipIK.GetComponent<MultiParentConstraint>().weight = 0;
            headIK.GetComponent<MultiAimConstraint>().weight = 0;
        }

        void Update()
        {
            // if (bicycleController._photonView.IsMine)
            // {
            if (cyclist != null && externalCharacter != null)
            {
#if UNITY_SWITCH
                bool tempAction = false;
                if (NintendoInput.isEditorInputActiv)
                {
                    tempAction = (NintendoInput.InputNpadButtonDown(NpadButton.Y) &&
                                  bicycleController.transform.InverseTransformDirection(bicycleController.rb.velocity)
                                      .z <=
                                  0.2f && waitTime == 0 && _isEnterBicycle);
                }
                else
                {
                    tempAction = (Input.GetKeyDown(KeyCode.Return) &&
                                  bicycleController.transform.InverseTransformDirection(bicycleController.rb.velocity)
                                      .z <=
                                  0.2f && waitTime == 0 && _isEnterBicycle);
                }

                if (tempAction)
#else
                if (Input.GetKeyDown(KeyCode.Return) &&
                    bicycleController.transform.InverseTransformDirection(bicycleController.rb.velocity).z <=
                    0.1f && waitTime == 0 && _isEnterBicycle)
#endif
                {
                    waitTime = 1.5f;
                    externalCharacter.transform.position =
                        cyclist.transform.root.position - transform.right * 0.5f +
                        transform.forward * 0.1f;
                    bicycleStatus.onBike = !bicycleStatus.onBike;
                    if (bicycleStatus.onBike)
                    {
                        if (prevLocalPosX < 0)
                            anim.Play("OnBike");
                        else
                            anim.Play("OnBikeFlipped");
                        StartCoroutine(AdjustRigWeight(0));
                    }
                    else
                    {
                        anim.Play("OffBike");
                        StartCoroutine(AdjustRigWeight(1));
                    }
                }

                prevLocalPosX = externalCharacter.transform.localPosition.x;
            }

            waitTime -= Time.deltaTime;
            waitTime = Mathf.Clamp(waitTime, 0, 1.5f);


            speed = bicycleController.transform.InverseTransformDirection(bicycleController.rb.velocity).z;
            isAirborne = bicycleController.isAirborne;
            anim.SetFloat("Speed", speed);
            anim.SetBool("isAirborne", isAirborne);
            if (bicycleStatus != null)
            {
                if (bicycleStatus.dislodged == false)
                {
                    if (!bicycleController.isAirborne && bicycleStatus.onBike)
                    {
                        clipInfoCurrent = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                        if (clipInfoCurrent == "IdleToStart" && clipInfoLast == "Idle")
                            StartCoroutine(LeftFootIK(0));
                        if (clipInfoCurrent == "Idle" && clipInfoLast == "IdleToStart")
                            StartCoroutine(LeftFootIK(1));
                        if (clipInfoCurrent == "Idle" && clipInfoLast == "Reverse")
                            StartCoroutine(LeftFootIdleIK(0));
                        if (clipInfoCurrent == "Reverse" && clipInfoLast == "Idle")
                            StartCoroutine(LeftFootIdleIK(1));

                        clipInfoLast = clipInfoCurrent;
                    }
                }
                else
                {
                    cyclist.SetActive(false);
#if UNITY_SWITCH
                    bool tempRespawn;
                    if (NintendoInput.isEditorInputActiv)
                    {
                        tempRespawn = NintendoInput.InputNpadButtonDown(NpadButton.X);
                    }
                    else
                    {
                        tempRespawn = Input.GetKeyDown(KeyCode.R);
                    }
                    if(tempRespawn)
#else
                    if (Input.GetKeyDown(KeyCode.R))
#endif
                    {
                        cyclist.SetActive(true);
                        bicycleStatus.dislodged = false;
                        bicycleStatus.onBike = true;
                        externalCharacter.SetActive(false);
                        cyclist.SetActive(true);
                        //
                        // cyclist.SetActive(false);
                        // externalCharacter.SetActive(true);
                        // externalCharacter.transform.position = new Vector3(
                        //     bicycleStatus.instantiatedRagdoll.transform.root.position.x,
                        //     bicycleStatus.instantiatedRagdoll.transform.root.position.y + 0.2f,
                        //     bicycleStatus.instantiatedRagdoll.transform.root.position.z);
                    }
                }
            }
            else
            {
                if (!bicycleController.isAirborne)
                {
                    clipInfoCurrent = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                    if (clipInfoCurrent == "IdleToStart" && clipInfoLast == "Idle")
                        StartCoroutine(LeftFootIK(0));
                    if (clipInfoCurrent == "Idle" && clipInfoLast == "IdleToStart")
                        StartCoroutine(LeftFootIK(1));
                    if (clipInfoCurrent == "Idle" && clipInfoLast == "Reverse")
                        StartCoroutine(LeftFootIdleIK(0));
                    if (clipInfoCurrent == "Reverse" && clipInfoLast == "Idle")
                        StartCoroutine(LeftFootIdleIK(1));

                    clipInfoLast = clipInfoCurrent;
                }
            }
            // }
        }

        IEnumerator LeftFootIK(int offset)
        {
            float t1 = 0f;
            while (t1 <= 1f)
            {
                t1 += Time.fixedDeltaTime;
                leftFootIK.GetComponent<TwoBoneIKConstraint>().weight =
                    Mathf.Lerp(-0.05f, 1.05f, Mathf.Abs(offset - t1));
                leftFootIdleIK.GetComponent<TwoBoneIKConstraint>().weight =
                    1 - leftFootIK.GetComponent<TwoBoneIKConstraint>().weight;
                yield return null;
            }
        }

        IEnumerator LeftFootIdleIK(int offset)
        {
            float t1 = 0f;
            while (t1 <= 1f)
            {
                t1 += Time.fixedDeltaTime;
                leftFootIdleIK.GetComponent<TwoBoneIKConstraint>().weight =
                    Mathf.Lerp(-0.05f, 1.05f, Mathf.Abs(offset - t1));
                yield return null;
            }
        }

        IEnumerator AdjustRigWeight(int offset)
        {
            StartCoroutine(LeftFootIK(1));
            if (offset == 0)
            {
                cyclist.SetActive(true);
                externalCharacter.SetActive(false);
            }

            float t1 = 0f;
            while (t1 <= 1f)
            {
                t1 += Time.deltaTime;
                rig.weight = Mathf.Lerp(-0.05f, 1.05f, Mathf.Abs(offset - t1));
                yield return null;
            }

            if (offset == 1)
            {
                yield return new WaitForSeconds(0.2f);
                cyclist.SetActive(false);
                externalCharacter.SetActive(true);
                // Matching position and rotation to the best possible transform to get a seamless transition
                externalCharacter.transform.position = cyclist.transform.root.position - transform.right * 0.5f +
                                                       transform.forward * 0.1f;
                externalCharacter.transform.rotation = Quaternion.Euler(
                    externalCharacter.transform.rotation.eulerAngles.x,
                    cyclist.transform.root.rotation.eulerAngles.y + 80,
                    externalCharacter.transform.rotation.eulerAngles.z);
            }
        }
        
    }
}