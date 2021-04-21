using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoader : MonoBehaviour
{
    
    public static DataLoader Instance;
    public List<GameObject> SavedObjects;
    public bool LoadingOnStart;
    public bool SavingOnEnd;

    private const string SAVE_KEY_DATA = "SavedData";
    private void Start()
    {
        Instance = this;
        if (LoadingOnStart)
        {
            var json = PlayerPrefs.GetString(SAVE_KEY_DATA);
            var jsonList = SaveHelper.GetJsonList(json);

            for (int i = 0; i < jsonList.Count; i++)
            {
                var jsonObj = jsonList[i];
                var baseGameObject = SavedObjects[i];
                SaveHelper.ParseToGameObject(jsonObj, baseGameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        if (SavingOnEnd)
        {
            var jsonList = new List<string>();
            for (int i = 0; i < SavedObjects.Count; i++)
            {
                var gameObj = SavedObjects[i];
                var json = SaveHelper.ParseToJson(gameObj);
                jsonList.Add(json);
            }
            PlayerPrefs.SetString(SAVE_KEY_DATA, SaveHelper.GetJson(jsonList));
        }
    }

    public class SaveHelper
    {
        private static char SPESIAL_INFO_SEPARATOR = '*';
        private static char GAMEOBJECT_SEPARATOR = '|';
        private const char JSON_SEPARATOR = '#';
        private static Dictionary<string, string> prefabOfTag = new Dictionary<string, string>
        {
            { "list", "list"},
            { "tree", "PartTree"},
            { "target", "target"},
            { "water", "Drop"},
            { "sugar", "Drop2"},
            { "stone", "Drop1"},
            { "root", "RootPart"}
        };

        //собирает всю инфу об объекте в json.
        public static string ParseToJson(GameObject gameObject)
        {//сохраняем всю инфу, затем переходим к его детям
            var res = new StringBuilder();

            res.Append(gameObject.tag);
            res.Append(SPESIAL_INFO_SEPARATOR);

            var position = gameObject.transform.localPosition;
            var rotation = gameObject.transform.rotation.eulerAngles;
            var localScale = gameObject.transform.localScale;
            WriteVector3(res, position);
            WriteVector3(res, rotation);
            WriteVector3(res, localScale);

            res.Append(gameObject.transform.childCount);

            var saveComponents = gameObject.GetComponents<MonoBehaviourSave>();
            foreach(var component in saveComponents)
            {
                res.Append(SPESIAL_INFO_SEPARATOR);
                res.Append(component.SaveData());
            }

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var child = gameObject.transform.GetChild(i);
                var json = ParseToJson(child.gameObject);
                res.Append(GAMEOBJECT_SEPARATOR);
                res.Append(json);
            }

            return res.ToString();
        }

        private static void WriteVector3(StringBuilder res, Vector3 vector)
        {
            res.Append(vector.x + " " + vector.y + " " + vector.z);
            res.Append(SPESIAL_INFO_SEPARATOR);
        }

        private static Vector3 getVector3(string str)
        {
            var items = str
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => float.Parse(x))
                .ToList();
            return new Vector3(items[0], items[1], items[2]);
        }

        public static void ParseToGameObject(string json, GameObject baseGameObject)
        {
            var jsons = json.Split(new char[] { GAMEOBJECT_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            var index = -1;
            ParseJson(jsons, baseGameObject, ref index);
        }

        public static void ParseJson(string[] jsons, GameObject parent, ref int index)
        {
            index += 1;
            var json = jsons[index];
            var info = json.Split(new char[] { SPESIAL_INFO_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            var strPos = info[1];
            var strRot = info[2];
            var strScale = info[3];
            var childCount = int.Parse(info[4]);
            var pos = getVector3(strPos);
            var rot = getVector3(strRot);
            var scale = getVector3(strScale);

            parent.transform.localPosition = pos;
            parent.transform.rotation = Quaternion.Euler(rot);
            parent.transform.localScale = scale;

            var saveComponent = parent.GetComponents<MonoBehaviourSave>();
            for(int i =0; i < saveComponent.Length; i++)
            {
                var data = info[5 + i];
                saveComponent[i].LoadData(data);
            }

            for (int i = 0; i < childCount; i++)
            {
                if(parent.transform.childCount > i)
                {
                    var child = parent.transform.GetChild(i);
                    ParseJson(jsons, child.gameObject, ref index);
                }
                else
                {
                    var nextInfo = jsons[index + 1].Split(new char[] { SPESIAL_INFO_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
                    var tag = nextInfo[0];
                    var prefabName = prefabOfTag[tag];
                    var prefab = Resources.Load(prefabName);
                    var InstObj = (GameObject)Instantiate(prefab, parent.transform);
                    ParseJson(jsons, InstObj, ref index);
                }
            }
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
    }
}
