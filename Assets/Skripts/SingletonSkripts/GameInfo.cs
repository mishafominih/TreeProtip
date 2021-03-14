using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public static GameInfo Instance { get; private set; }

    public Stock sugar { get; private set; }
    public Stock water { get; private set; }
    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sugar = TreeSkript.Instance.GetStocks()[0];
        water = TreeSkript.Instance.GetStocks()[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
