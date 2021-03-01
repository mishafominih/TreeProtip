using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValue : MonoBehaviour
{
    public Stock stock;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = ((int)stock.Get()).ToString();
        if (stock.IsMax()) text.color = Color.red;
        else text.color = Color.black;
    }
}
