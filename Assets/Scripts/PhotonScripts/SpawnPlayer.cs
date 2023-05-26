using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject _playerPrefab;
    public GameObject _spawnPoint;
    
    void Awake()
    {
        Vector3 randomPos = new Vector3(_spawnPoint.transform.position.x + Random.Range(-3, 4), _spawnPoint.transform.position.y, _spawnPoint.transform.position.z);
        PhotonNetwork.Instantiate(_playerPrefab.name, randomPos, Quaternion.identity);
    }
}
