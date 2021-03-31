using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PolutionListSkript : MonoBehaviour
{
    public float PolutionTime;

    private float timer = 0;
    private const float percentColorPolution = 0.4f;
    private float percentPolution;

    private Vector3 autumn = new Vector3(255, 155, 0);

    private SpriteRenderer sprite;
    private PolygonCollider2D collider;
    private TargetSkript target;
    void Start()
    {
        target = GetComponent<TargetSkript>();
        collider = GetComponent<PolygonCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (target.target == null) return;
        if (target.target.GetComponent<Fall>() != null) return;
        if (timer < PolutionTime) timer += Time.deltaTime;
        else timer = PolutionTime;

        if (Input.touchCount == 1 || Input.GetMouseButton(0))
        {
            var touch = CameraMove.Instance.GetComponent<Camera>()
                .ScreenToWorldPoint(GetTouch());
            if (collider.OverlapPoint(touch))
            {
                timer = 0;
            }
        }

        percentPolution = (timer / PolutionTime);
        CalculatePolution();

        if (RainSkript.Instance.IsRain())
        {
            timer = 0;
        }
    }

    private void CalculatePolution()
    {
        var startColor = getAutumnPolution();
        var deltaColor = (1 - percentColorPolution) * (1 - percentPolution);
        var newColor = startColor * (deltaColor + percentColorPolution)/255;
        sprite.color = new Color(newColor.x, newColor.y, newColor.z);
    }

    private Vector3 getAutumnPolution()
    {
        var start = new Vector3(255, 255, 255);
        var time = Seazons.Instance.WinterTime;
        var timer = Seazons.Instance.GetTime();
        var persent = timer / time;
        var delta = (start - autumn) * (1-persent);
        return autumn + delta;
    }

    public float GetProfit(float value)
    {
        return value * (1 - percentPolution);
    }


    private static Vector2 GetTouch()
    {
        if (Input.touchCount > 0) return Input.GetTouch(0).position;
        return Input.mousePosition;
    }
}
