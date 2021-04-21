using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSkript : MonoBehaviour
{
    public GameObject target;


    private Camera cam;
    private void Awake()
    {
    }

    private void OnDisable()
    {
        this.enabled = true;
    }

    void Start()
    {
        if(transform.parent != null)
        {
            target = transform.parent.gameObject;
        }
        cam = CameraMove.Instance.GetComponent<Camera>();
        if (target != null && gameObject.tag == "list") SetLink();
    }

    private void SetLink()
    {
        var prefab = Resources.Load<GameObject>("list");
        target.GetComponent<Link>().SetObject(gameObject, prefab);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
        if (gameObject.tag == "list") SetLink();
    }

    void Update()
    {
        var newPos = transform.position;
        if (target == null)
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0).position;
                newPos = hit(newPos, touch);
            }
            else if (Input.GetMouseButton(0))
            {
                var touch = Input.mousePosition;
                newPos = hit(newPos, touch);
            }
        }
        else
        {
            newPos = new Vector3();
        }
        transform.localPosition = new Vector3(newPos.x, newPos.y, 0);
    }

    private Vector3 hit(Vector3 newPos, Vector2 touch)
    {
        var area = GameObject.FindGameObjectWithTag("Area").
                            GetComponent<PolygonCollider2D>();
        if (area.OverlapPoint(touch))
        {
            var pos = cam.ScreenToWorldPoint(touch);
            newPos = new Vector3(pos.x, pos.y + 0.5f, 0);
        }

        return newPos;
    }
}
