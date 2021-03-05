using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSkript : MonoBehaviour
{
    public GameObject target;
    public Camera cam;

    private GameObject jostick;
    void Start()
    {
        jostick = GameObject.FindGameObjectWithTag("jostick");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        var newPos = transform.position;
        if (target == null)
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0).position;
                var area = GameObject.FindGameObjectWithTag("Area").
                    GetComponent<BoxCollider2D>();
                var jarea = jostick.GetComponent<BoxCollider2D>();
                if (area.bounds.Contains(touch) && !jarea.bounds.Contains(touch))
                {
                    var pos = cam.ScreenToWorldPoint(touch);
                    newPos = new Vector3(pos.x, pos.y + 0.5f, 0);
                }
            }
        }
        else
            newPos = target.transform.position;
        transform.position = new Vector3(newPos.x, newPos.y, 0);
    }
}
