using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GrowButtonSkript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            var tree = TreeSkript.Instance;
            var sugar = GameInfo.Instance.sugar;
            var water = GameInfo.Instance.water;
            if (sugar.TakePart(tree.GetCast().ToArray()[0]))
            {
                if (water.TakePart(tree.GetCast().ToArray()[1]))
                {
                    TreeSkript.Instance.Grow();
                }
                else
                {
                    sugar.AddPart(tree.GetCast().ToArray()[0]);
                }
            }
        });
    }
}
