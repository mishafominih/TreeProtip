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
        sugar = TreeSkript.Instance.GetStocks()[0];
        water = TreeSkript.Instance.GetStocks()[1];
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
