using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTreeGrow : MonoBehaviour
{
    void Start()
    {
        GameInfo.treeGrow.Subscribe(() => {
            GetComponent<LavelInfo>().Increment();
            GameInfo.Instance.sugar.MaxValue *= 1.2f;
            GameInfo.Instance.water.MaxValue *= 1.2f;
            //var step = TreeSkript.Instance.Step;
            //transform.localScale = new Vector3(
            //    transform.localScale.x + step,
            //    transform.localScale.y + step);
        });
    }
}
