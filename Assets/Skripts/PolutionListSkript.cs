using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PolutionListSkript : MonoBehaviour
{
    public float PolutionTime;

    private SpriteRenderer sprite;
    private float timer = 0;
    private Stock sugar;
    private Stock water;
    private const int maxColor = 255;
    private const int minColor = 100;
    private float percentPolution;
    private PolygonCollider2D collider;
    private TargetSkript target;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetSkript>();
        collider = GetComponent<PolygonCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        sugar = GameInfo.Instance.sugar;
        water = GameInfo.Instance.water;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.target == null) return;
        if(timer < PolutionTime) timer += Time.deltaTime;
        else timer = PolutionTime;

        if(Input.touchCount == 1)
        {
            var touch = CameraMove.Instance.GetComponent<Camera>()
                .ScreenToWorldPoint(Input.GetTouch(0).position);
            if (collider.OverlapPoint(touch))
            {
                timer = 0;
            }
        }

        percentPolution = (timer / PolutionTime);
        var deltaColor = (maxColor - minColor) * (1 - percentPolution);
        var newColor = (minColor + deltaColor) / maxColor;
        sprite.color = new Color(newColor, newColor, newColor);

        if (RainSkript.Instance.IsRain())
        {
            timer = 0;
        }
    }

    public float GetProfit(float value)
    {
        return value * (1 - percentPolution);
    }
}
