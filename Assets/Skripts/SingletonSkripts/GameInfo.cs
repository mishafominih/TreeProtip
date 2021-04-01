using System.Collections;
using System.Collections.Generic;
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
        sugar = TreeSkript.Instance.GetStocks()[0];
        water = TreeSkript.Instance.GetStocks()[1];
    }
    private void Start()
    {
    }
}
