using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public float Delta = 2;
    public void AddWater()
    {
        var level = GetComponent<LavelInfo>().Lavel;
        GameInfo.Instance.water.AddPart(Delta * Time.deltaTime * level * level);
    }
}
