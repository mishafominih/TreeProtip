using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoader : MonoBehaviour
{
    public static DataLoader Instance;
    public List<GameObject> SavedObjects;
    public bool LoadingOnStart;
    public bool SavingOnEnd;

    private const string SAVE_KEY = "CountSavedObject";

    private void Start()
    {
        Instance = this;
        if (LoadingOnStart)
        {
            foreach (var saved in SavedObjects)
                Destroy(saved);
            var count = PlayerPrefs.GetInt(SAVE_KEY);
            for (int i = 0; i < count; i++)
            {
                Instantiate(Resources.Load(i.ToString()));
            }
            GameInfo.loadData.Publish();
        }
    }

    private void OnApplicationQuit()
    {
        if (SavingOnEnd)
        {
            for(int i = 0; i < SavedObjects.Count; i++)
            {
                PrefabUtility.CreatePrefab($"Assets/Resources/{i}.prefab", SavedObjects[i]);
            }
            PlayerPrefs.SetInt("CountSavedObject", SavedObjects.Count);
            GameInfo.saveData.Publish();
        }
    }
}
