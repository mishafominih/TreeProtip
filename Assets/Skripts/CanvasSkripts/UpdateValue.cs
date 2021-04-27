﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValue : MonoBehaviour
{
    public bool isSugar;

    private Stock stock;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        stock = getStock();
        int value = Mathf.RoundToInt(stock.GetValue());
        text.text = value.ToString();
        if (value == Mathf.RoundToInt(stock.MaxValue)) text.color = Color.red;
        else text.color = Color.black;
    }

    private Stock getStock()
    {
        if (isSugar) return GameInfo.Instance.sugar;
        return GameInfo.Instance.water;
    }
}
