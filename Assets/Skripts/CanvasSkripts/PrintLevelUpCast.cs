using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PrintLevelUpCast : MonoBehaviour
{
    public GameObject g;
    public bool isTree;
    public int index;

    Text text;
    Stock sugar;
    Stock water;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        sugar = GameInfo.Instance.sugar;
        water = GameInfo.Instance.water;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTree)
        {
            var casts = g.GetComponent<TreeSkript>().GetCast().ToList();
            Do(casts);
        }
        else
        {
            var casts = g.GetComponent<KorenSkript>().GetCast().ToList();
            Do(casts);
        }
    }

    private void Do(List<float> casts)
    {
        var str = text.text.Split(':');
        var res = str[0];
        res += ':';
        res += (int)casts[index];
        text.text = res;

        var stock = index == 0 ? sugar : water;
        if (stock.isEnough((int)casts[index])) text.color = Color.red;
        else text.color = Color.black;
    }
}
