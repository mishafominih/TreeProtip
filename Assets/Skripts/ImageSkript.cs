using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImageSkript : MonoBehaviour
{
    public float SugarKof;
    public float WaterKof;
    public ButtonSkript btn;
    public GameObject link;
    Stock sugar;
    Stock water;
    // Start is called before the first frame update
    void Start()
    {
        sugar = GameObject.Find("sugar").GetComponent<Stock>();
        water = GameObject.Find("water").GetComponent<Stock>();
    }

    public IEnumerable<float> GetCast()
    {//вначале сахар потом вода
        var z = GameObject.FindGameObjectsWithTag("stock")
            .Select(x => x.GetComponent<Stock>().MaxValue)
            .OrderByDescending(x => x).ToList();
        z[0] = z[0] * SugarKof;
        z[1] = z[1] * WaterKof;
        return z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var v = GetComponent<BoxCollider2D>();
            if (v.bounds.Contains(touch.position))
            {
                if (sugar.TakePart(GetCast().ToArray()[0]))
                {
                    if (water.TakePart(GetCast().ToArray()[1]))
                    {
                        btn.Click(link, touch);
                    }
                    else
                    {
                        sugar.AddPart(GetCast().ToArray()[0]);
                    }
                }
            }
        }
    }
}
