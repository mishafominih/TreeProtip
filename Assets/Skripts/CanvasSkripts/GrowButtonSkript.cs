using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GrowButtonSkript : MonoBehaviour
{
    public TreeSkript tree;
    // Start is called before the first frame update
    void Start()
    {
        var sugar = GameInfo.Instance.sugar;
        var water = GameInfo.Instance.water;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (sugar.TakePart(tree.GetCast().ToArray()[0]))
            {
                if (water.TakePart(tree.GetCast().ToArray()[1]))
                {
                    foreach (var g in GameObject.FindGameObjectsWithTag("tree"))
                    {
                        g.GetComponent<TreeSkript>().Grow();
                    }
                }
                else
                {
                    sugar.AddPart(tree.GetCast().ToArray()[0]);
                }
            }
        });
    }
}
