using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KorenSkript : MonoBehaviour
{
    public float Step;
    Stock sugar;
    Stock water;
    private int level = 1;
    void Start()
    {
        sugar = GameObject.Find("sugar").GetComponent<Stock>();
        water = GameObject.Find("water").GetComponent<Stock>();
    }

    // Update is called once per frame
    void Update()
    {
        water.AddPart(0.01f * level * level);
    }

    public IEnumerable<float> GetCast()
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
        if (sugar.TakePart(GetCast().ToArray()[0])){
            if (water.TakePart(GetCast().ToArray()[1]))
            {
                transform.localScale = new Vector3(
                    transform.localScale.x + Step,
                    transform.localScale.y + Step);
                level++;
            }
            else{
                sugar.AddPart(GetCast().ToArray()[0]);
            }
        }
    }
}
