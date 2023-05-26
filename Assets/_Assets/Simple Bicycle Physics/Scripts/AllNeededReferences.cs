using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using SBPScripts;
using UnityEngine;

public class AllNeededReferences : MonoBehaviour
{
    public PhotonView _PhotonView;
    public BicycleController _BicycleController;
    public BicycleStatus _BicycleStatus;
    public GameObject _ExternalCharacter;
    public PerfectMouseLook _PerfectMouseLook;

    private void Awake()
    {
        _PhotonView = GetComponent<PhotonView>();
    }
}
