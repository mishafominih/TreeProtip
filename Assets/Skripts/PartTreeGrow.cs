using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTreeGrow : MonoBehaviour
{
    void Start()
    {
        GameInfo.treeGrow.Subscribe(NewMethod);
    }
    private void OnDestroy()
    {
        GameInfo.treeGrow.Delete(NewMethod);
    }
    private void NewMethod()
    {
        GetComponent<LavelInfo>().Increment();
        GameInfo.Instance.sugar.MaxValue *= 1.2f;
        GameInfo.Instance.water.MaxValue *= 1.2f;
    }
}
