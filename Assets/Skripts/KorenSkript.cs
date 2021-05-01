using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KorenSkript : MonoBehaviour
{
    public static KorenSkript Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public List<float> GetCast()
    {//вначале сахар потом вода
        var lavel = GetComponent<LavelInfo>();
        return new List<float>
        {
            lavel.Lavel * lavel.Lavel * 10,
            0
        };
    }

    public void Grow()
    {
        var sugar = GameInfo.Instance.sugar;
        var water = GameInfo.Instance.water;
        if (sugar.GetValue() >= GetCast()[0]){
            if (water.GetValue() >= GetCast()[1])
            {
                sugar.TakePart(GetCast()[0]);
                water.TakePart(GetCast()[1]);
                foreach (var root in GameObject.FindGameObjectsWithTag("root"))
                {
                    root.GetComponent<KorenGrowSkript>().Grow();
                }
            }
        }
    }
}
