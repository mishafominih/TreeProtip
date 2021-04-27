using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listSkript : MonoBehaviour
{
    public float Water = 0.1f;
    public float delta = 0.1f;

    private PolutionListSkript polution;
    private TargetSkript target;
    void Start()
    {
        target = GetComponent<TargetSkript>();
        polution = GetComponent<PolutionListSkript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Seazons.Instance.IsSummer && target.OnTree())
        {
            if (GameInfo.Instance.water.isEnough(Water * Time.deltaTime))
            {
                GameInfo.Instance.water.TakePart(Water * Time.deltaTime);
                GameInfo.Instance.sugar.AddPart(delta * Time.deltaTime);
            }
            else
            {
                polution.RegisterLoseWater();
            }
        }
    }
}
