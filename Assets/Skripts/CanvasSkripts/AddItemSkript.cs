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

    public bool IsWinterBuild;
    public ButtonSkript btn;
    public GameObject link;

    //int lavel = 1;
    Stock sugar;
    Stock water;
    // Start is called before the first frame update
    void Start()
    {
        sugar = GameInfo.Instance.sugar;
        water = GameInfo.Instance.water;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!Seazons.Instance.IsSummer && !IsWinterBuild)
            {
                btn.hide();
                return;
            }

            if (sugar.GetValue() >= GetCast()[0] && water.GetValue() >= GetCast()[1] && btn.IsFree())
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
