using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public static GameInfo Instance { get; private set; }

    public static OnSummerEvent IsSummer;
    public static OnWinterEvent IsWinter;
    public static TreeGrow treeGrow;
    private static SaveData saveData;
    private static LoadData loadData;

    public static void RegisterSaveEvents(Action<StringBuilder> save, Action<string> load)
    {
        saveData.Subscribe(save);
        loadData.Subscribe(load);
    }

    public static void Load(string data, char separator)
    {
        loadData.Publish(data, separator);
    }

    public static string Save(char separator)
    {
        return saveData.Publish(separator);
    }

    public Stock sugar { get; private set; }
    public Stock water { get; private set; }
    public void Awake()
    {
        Instance = this;
        IsSummer = new OnSummerEvent();
        IsWinter = new OnWinterEvent();
        saveData = new SaveData();
        loadData = new LoadData();
        treeGrow = new TreeGrow();
    }

    private void Start()
    {
        sugar = TreeSkript.Instance.GetStocks()[0];
        water = TreeSkript.Instance.GetStocks()[1];
    }
    private void Update()
    {
        if(sugar == null)
            sugar = TreeSkript.Instance.GetStocks()[0];
        if(water == null) 
            water = TreeSkript.Instance.GetStocks()[1];
    }
}
