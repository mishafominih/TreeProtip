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
        GameInfo.RegisterSaveEvents(
        stream =>
        {
            stream.Append(value);
            stream.Append(" ");
            stream.Append(MaxValue);
        },
        data =>
        {
            var info = data.Split(' ');
            value = float.Parse(info[0]);
            MaxValue = float.Parse(info[1]);
        });
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

    public float GetValue() => value;

    public void AddStart() => MaxValue += StartMaxValue;

    public bool isEnough(float val) => value >= val;

    public bool IsMax() => value == MaxValue;

    private void Control()
    {
        if(value > MaxValue) value = MaxValue;
    }
}
