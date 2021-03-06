using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KorenSkript : MonoBehaviour
{
    Stock sugar;
    Stock water;
    void Start()
    {
        sugar = GameObject.Find("sugar").GetComponent<Stock>();
        water = GameObject.Find("water").GetComponent<Stock>();
    }

    // Update is called once per frame
    void Update()
    {
        var level = GetComponent<KorenGrowSkript>().GetLavel();
        water.AddPart(0.01f * level * level);
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
