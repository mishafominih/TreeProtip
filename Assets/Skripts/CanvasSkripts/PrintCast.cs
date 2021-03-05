using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PrintCast : MonoBehaviour
{
    public GameObject g;
    public int index;

    Text text;
    Stock sugar;
    Stock water;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        sugar = GameObject.Find("sugar").GetComponent<Stock>();
        water = GameObject.Find("water").GetComponent<Stock>();
    }

    // Update is called once per frame
    void Update()
    {
        var casts = g.GetComponent<AddItemSkript>().GetCast().ToList();
        Do(casts);
    }

    private void Do(List<float> casts)
    {
        var str = text.text.Split(':');
        var res = str[0];
        res += ':';
        res += (int)casts[index];
        text.text = res;

        var stock = index == 0 ? sugar : water;
        if (stock.isEnough((int)casts[index])) text.color = Color.red;
        else text.color = Color.black;
    }
}
