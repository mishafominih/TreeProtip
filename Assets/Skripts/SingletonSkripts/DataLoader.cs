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
    public GameObject test;

    private const string SAVE_KEY = "SavedData";
    private const char SEPARATOR = '|';
    private void Start()
    {
        Instance = this;
        if (LoadingOnStart && PlayerPrefs.HasKey(SAVE_KEY))
        {
            var saveStr = PlayerPrefs.GetString(SAVE_KEY);
            var parse = saveStr.Split(new char[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < parse.Length; i++)
            {
                Object.Parse(parse[i], SavedObjects[i]);
            }
        }
    }
    private void OnApplicationQuit()
    {
        if (!SavingOnEnd) return;
        var result = SavedObjects
            .Select(x => new Object(x).ToString())//EditorJsonUtility.ToJson(x.transform, true))
            .ToList();
        var saveStr = "";
        if(result.Count > 1)
            saveStr = result.Aggregate((f, s) => SEPARATOR + s);
        saveStr = result.First() + saveStr;
        PlayerPrefs.SetString(SAVE_KEY, saveStr);
    }

    public class Object
    {
        public static char SEPARATOR = '+';
        private List<Component> components;
        private GameObject head;
        public Object(GameObject gameObject)
        {
            head = gameObject;
            components = gameObject.GetComponents<Component>().ToList();
        }

        public override string ToString()
        {
            var res = new StringBuilder();
            var gameObjectJson = EditorJsonUtility.ToJson(head, true);
            res.Append(gameObjectJson);
            foreach(var component in components)
            {
                res.Append(SEPARATOR);
                var json = EditorJsonUtility.ToJson(component, true);
                res.Append(json);
            }
            return res.ToString();
        }

        public static void Parse(string json, GameObject prefab)
        {
            var strs = json.Split(SEPARATOR);
            var head = strs[0];
            var components = strs.Skip(1).ToList();
            EditorJsonUtility.FromJsonOverwrite(head, prefab);
            var originComponents = prefab.GetComponents<Component>();
            for(int i = 0; i < originComponents.Length; i++)
            {
                EditorJsonUtility.FromJsonOverwrite(components[i], originComponents[i]);
            }
        }
    }
}
