using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddItemSkript : MonoBehaviour
{
    public float StartSugar;
    public float StartWater;
    public float StepSugar;
    public float StepWater;

    public ButtonSkript btn;
    public GameObject link;

    //int lavel = 1;
    Stock sugar;
    Stock water;
    // Start is called before the first frame update
    void Start()
    {
        sugar = GameObject.Find("sugar").GetComponent<Stock>();
        water = GameObject.Find("water").GetComponent<Stock>();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (sugar.Get() >= GetCast()[0] && water.Get() >= GetCast()[1])
            {
                sugar.TakePart(GetCast()[0]);
                water.TakePart(GetCast()[1]);

                StartSugar += StepSugar;
                StartWater += StepWater;

                btn.Click(link);
            }
        });
    }

    public float[] GetCast()
    {//вначале сахар потом вода
        return new float[]
        {
            StartSugar,
            StartWater
        };
    }
}
