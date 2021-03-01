using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listSkript : MonoBehaviour
{
    Stock sugar;
    void Start()
    {
        sugar = GameObject.Find("sugar").GetComponent<Stock>();
    }

    // Update is called once per frame
    void Update()
    {
        sugar.AddPart(0.1f);
    }
}
