﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KorenSkript : MonoBehaviour
{
    public static KorenSkript Instance;
    public float Delta = 2;
    Stock sugar;
    Stock water;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        sugar = GameInfo.Instance.sugar;
        water = GameInfo.Instance.water;
    }

    // Update is called once per frame
    void Update()
    {
        var level = GetComponent<LavelInfo>().Lavel;
        water.AddPart(Delta * Time.deltaTime * level * level);
    }

    public List<float> GetCast()
    {//вначале сахар потом вода
        var z = GameObject.FindGameObjectsWithTag("stock")
            .Select(x => x.GetComponent<Stock>().MaxValue)
            .OrderByDescending(x => x).ToList();
        z[0] = z[0] * 0.4f;
        z[1] = z[1] * 0.25f;
        return z;
    }

    public void Grow()
    {
        if (sugar.Get() >= GetCast()[0]){
            if (water.Get() >= GetCast()[1])
            {
                sugar.TakePart(GetCast()[0]);
                sugar.TakePart(GetCast()[1]);
                foreach (var root in GameObject.FindGameObjectsWithTag("root"))
                {
                    root.GetComponent<KorenGrowSkript>().Grow();
                }
            }
        }
    }
}
