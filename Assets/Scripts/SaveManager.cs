using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private string keySave = "SaveData";
    public SaveData saveData = new SaveData();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        GameObject temp = new GameObject("SaveManager");
        temp.AddComponent<SaveManager>();
    } 
        
    private void Awake()
    {
        instance = this;
        saveData = LoadData();
    }

    public void SaveData()
    {
        throw new NotImplementedException();
    }

    public SaveData LoadData()
    {
        throw new NotImplementedException();
    }

}

public class SaveData
{
    public int money = 2000;
    public float averagRating = 0;
    public int[] rating = new int[6]{0,0,0,0,0,0};
}