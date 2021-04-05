using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    private bool first = true;
    private Quaternion rotation;
    private float scale = 1;

    public float GetScale() => scale;
    public void SetObject(GameObject obj, GameObject prefab)
    {
        if (obj.tag == "list" && first)
        {
            scale = obj.transform.localScale.x;
            first = false;
            GameInfo.IsSummer.Subscribe(() =>
            {
                obj = Instantiate(prefab);
                obj.GetComponent<SquareController>().enabled = false;
                obj.transform.rotation = rotation;
                obj.AddComponent<GrowList>();
                obj.GetComponent<TargetSkript>().target = gameObject;
                obj.transform.SetParent(transform);
            });
            GameInfo.IsWinter.Subscribe(() =>
            {
                rotation = obj.transform.rotation;
                var fallPoint = Instantiate(Resources.Load<GameObject>("FallPoint"), transform.position, transform.rotation);
                var target = obj.GetComponent<TargetSkript>();
                fallPoint.GetComponent<Fall>().link = obj;
                target.target = fallPoint;
                obj.transform.SetParent(fallPoint.transform);
            });
            GameInfo.treeGrow.Subscribe(() =>
            {
                var step = TreeSkript.Instance.Step;
                var size = TreeSkript.Instance.transform.localScale.x;
                var oldSize = size - step;
                var percent = size / oldSize;
                scale = scale / percent;
                if(obj != null)
                {
                    obj.transform.localScale = new Vector3(scale, scale, scale);
                }
            });
        }
    }
}
