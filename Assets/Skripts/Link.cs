using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    private bool first = true;
    private Quaternion rotation;
    public void SetObject(GameObject obj, GameObject prefab)
    {
        if (obj.tag == "list" && first)
        {
            first = false;
            GameInfo.IsSummer.Subscribe(() =>
            {
                obj = Instantiate(prefab);
                obj.GetComponent<SquareController>().enabled = false;
                obj.transform.rotation = rotation;
                obj.AddComponent<GrowList>();
                obj.GetComponent<TargetSkript>().target = gameObject;
            });
            GameInfo.IsWinter.Subscribe(() =>
            {
                rotation = obj.transform.rotation;
                var fallPoint = Instantiate(Resources.Load<GameObject>("FallPoint"), transform.position, transform.rotation);
                var target = obj.GetComponent<TargetSkript>();
                fallPoint.GetComponent<Fall>().link = obj;
                target.target = fallPoint;
            });
        }
    }
}
