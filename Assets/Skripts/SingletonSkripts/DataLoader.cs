using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoader : MonoBehaviour
{
    //из кода добавлять только объекты, не присутствующие при старте сцены и не имеющие родителей!!!
    public static DataLoader Instance;
    public List<GameObject> SavedObjects;
    public bool LoadingOnStart;
    public bool SavingOnEnd;

    private const string SAVE_KEY_COUNT = "CountSavedObject";
    private const string SAVE_KEY_DATA = "SavedData";
    private void Start()
    {
        Instance = this;
        if (LoadingOnStart)
        {
            foreach (var saved in SavedObjects)
                Destroy(saved);
            var count = PlayerPrefs.GetInt(SAVE_KEY_COUNT);
            var json = PlayerPrefs.GetString(SAVE_KEY_DATA);
            var jsonList = SaveHelper.GetJsonList(json);
            for (int i = 0; i < count; i++)
            {
                var obj = Resources.Load(i.ToString());
                var gameObj = Instantiate(obj);
                //SaveHelper.ParseToGameObject(jsonList[i], (GameObject)gameObj);
            }
            GameInfo.loadData.Publish();
        }
    }

    private void OnApplicationQuit()
    {
        if (SavingOnEnd)
        {
            var jsonList = new List<string>();
            for(int i = 0; i < SavedObjects.Count; i++)
            {
                var gameObj = SavedObjects[i];
                PrefabUtility.CreatePrefab($"Assets/Resources/{i}.prefab", gameObj);
                jsonList.Add(SaveHelper.ParseToJson(gameObj));
            }
            PlayerPrefs.SetInt(SAVE_KEY_COUNT, SavedObjects.Count);
            PlayerPrefs.SetString(SAVE_KEY_DATA, SaveHelper.GetJson(jsonList));
            GameInfo.saveData.Publish();
        }
    }

    public class SaveHelper
    {
        private static char COMPONENT_SEPARATOR = '+';
        private static char GAMEOBJECT_SEPARATOR = '|';
        private const char JSON_SEPARATOR = '#';

        //собирает всю инфу об объекте в json.
        public static string ParseToJson(GameObject prefab)
        {
            var res = new StringBuilder();
            res.Append(Parse(prefab));
            if (prefab.transform.childCount == 0) return res.ToString();
            for(int i = 0; i < prefab.transform.childCount; i++)
            {
                var child = prefab.transform.GetChild(i);
                var json = ParseToJson(child.gameObject);
                res.Append(GAMEOBJECT_SEPARATOR);
                res.Append(json);
            }
            return res.ToString();
        }

        //обновляет всю информацию о префабе
        public static void ParseToGameObject(string json, GameObject gameObj)
        {
            var jsonList = json.Split(new char[] { GAMEOBJECT_SEPARATOR }).ToList();
            var index = 0;
            UpdGameObject(jsonList, gameObj, ref index);
        }

        //превращает несколько json в один
        public static string GetJson(List<string> jsonList)
        {
            var json = jsonList.First();
            for (int i = 1; i < jsonList.Count; i++)
            {
                json += JSON_SEPARATOR;
                json += jsonList[i];
            }
            return json;
        }

        //превращает один json с информацией обо всех объектах в несколько json
        public static List<string> GetJsonList(string json)
        {
            return json
                .Split(new char[] { JSON_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        //возвращает json представление объекта и его компонентов не учитывая дочерние элементы.
        private static string Parse(GameObject prefab)
        {
            var res = new StringBuilder();
            var gameObjectJson = EditorJsonUtility.ToJson(prefab, true);
            res.Append(gameObjectJson);
            foreach (var component in prefab.GetComponents<Component>())
            {
                res.Append(COMPONENT_SEPARATOR);
                var json = EditorJsonUtility.ToJson(component, true);
                res.Append(json);
            }
            return res.ToString();
        }

        //рекурсивно обновляет информацию обо всех детях объекта
        private static void UpdGameObject(List<string> jsonList, GameObject gameObj, ref int index)
        {
            toGameObject(jsonList[index], gameObj);
            if (gameObj.transform.childCount == 0) return;
            for (int i = 0; i < gameObj.transform.childCount; i++)
            {
                var child = gameObj.transform.GetChild(i).gameObject;
                index += 1;
                UpdGameObject(jsonList, child, ref index);
            }
        }

        //обновляет информацию только об этом объекте не учитывая его детей
        private static void toGameObject(string json, GameObject prefab)
        {
            var strs = json.Split(COMPONENT_SEPARATOR);
            var head = strs[0];
            var components = strs.Skip(1).ToList();
            EditorJsonUtility.FromJsonOverwrite(head, prefab);
            var originComponents = prefab.GetComponents<Component>();
            for (int i = 0; i < components.Count; i++)
            {
                EditorJsonUtility.FromJsonOverwrite(components[i], originComponents[i]);
            }
        }
    }
}
