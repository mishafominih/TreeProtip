using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    public float MaxValue;

    private float StartMaxValue;
    private float value;

    private void Start()
    {
        StartMaxValue = MaxValue;
    }

    public bool TakePart(float part)
    {
        if (value < part) return false;
        value -= part;
        return true;
    }

    public void AddPart(float part)
    {
        value += part;
        Control();
    }

    public float Get() => value;

    public void AddStart() => MaxValue += StartMaxValue;

    public bool isEnough(float val) => value >= val;

    public bool IsMax() => value == MaxValue;

    private void Control()
    {
        if(value > MaxValue) value = MaxValue;
    }
}
