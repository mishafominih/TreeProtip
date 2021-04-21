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

    public Stock sugar { get; private set; }
    public Stock water { get; private set; }
    public void Awake()
    {
        Instance = this;
        IsSummer = new OnSummerEvent();
        IsWinter = new OnWinterEvent();
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
