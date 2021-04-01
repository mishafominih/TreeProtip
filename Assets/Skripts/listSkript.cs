using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listSkript : MonoBehaviour
{
    public float delta = 0.1f;

    private PolutionListSkript polution;
    void Start()
    {
        polution = GetComponent<PolutionListSkript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Seazons.Instance.IsSummer)
            GameInfo.Instance.sugar.AddPart(polution.GetProfit(delta * Time.deltaTime));
    }
}
