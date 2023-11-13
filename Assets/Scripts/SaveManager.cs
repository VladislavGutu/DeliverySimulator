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
        DontDestroyOnLoad(temp);
    } 
        
    private void Awake()
    {
        instance = this;
        LoadData();        
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Money", saveData.money);
        for (int i = 0; i < saveData.rating.Length; i++)
        {
            PlayerPrefs.SetInt($"Rating{i}",saveData.rating[i]);
        }
    }

    public void LoadData()
    {
        saveData.money = PlayerPrefs.GetInt("Money", 100);
        for (int i = 0; i < saveData.rating.Length; i++)
        {
            saveData.rating[i] = PlayerPrefs.GetInt($"Rating{i}");
        }
    }

}

public class SaveData

{
    public int money = 100;
    public float averagRating = 0;
    public int[] rating = new int[6]{0,0,0,0,0,0};
}