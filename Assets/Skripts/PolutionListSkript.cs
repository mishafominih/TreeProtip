using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PolutionListSkript : MonoBehaviour
{
    public int MaxLose = 1500;

    private const float percentColorPolution = 0.4f;
    private float percentPolution;
    private SpriteRenderer sprite;
    private TargetSkript target;
    private float maxColor;
    private float minColor = 90;
    private int lose;
    void Start()
    {
        target = GetComponent<TargetSkript>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        maxColor = sprite.color.r * 255;
    }

    void Update()
    {
        if (!target.OnTree()) return;
        
        percentPolution = (lose / (float)MaxLose);
        var deltaColor = (maxColor - minColor) * (1 - percentPolution);
        var newColor = (minColor + deltaColor) / maxColor;
        sprite.color = new Color(newColor, newColor, newColor);

        if (lose >= MaxLose)
        {
            target.target.GetComponent<Link>().Die();
        }

    }

    public void RegisterLoseWater()
    {
        lose++;
    }
}
