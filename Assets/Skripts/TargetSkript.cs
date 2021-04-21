using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSkript : MonoBehaviour
{
    public GameObject target;

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
            var camera = CameraMove.Instance.transform;
            newPos = new Vector3(camera.position.x, camera.position.y);
        }
        else
        {
            newPos = new Vector3();
        }
        transform.localPosition = new Vector3(newPos.x, newPos.y, 0);
    }
}
