using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listSkript : MonoBehaviour
{
    public float delta = 0.1f;
    public Stock sugar;

    private PolutionListSkript polution;
    void Start()
    {
        polution = GetComponent<PolutionListSkript>();
        sugar = GameInfo.Instance.sugar;
    }

    // Update is called once per frame
    void Update()
    {
        sugar.AddPart(polution.GetProfit(delta));
    }
}
