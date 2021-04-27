using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    private bool first = true;
    private Quaternion rotation;
    private float scale = 1;
    private GameObject prefab;
    private GameObject obj;
    public float GetScale() => scale;

    public void Die()
    {
        if (obj.tag == "list")
        {
            GameInfo.IsSummer.Delete(Summer);
            GameInfo.IsWinter.Delete(Winter);
            GameInfo.treeGrow.Delete(treeGrow);
        }
        Winter();
        Destroy(gameObject);
    }

    public void SetObject(GameObject objt, GameObject prfb)
    {
        obj = objt;
        prefab = prfb;
        if (obj.tag == "list" && first)
        {
            scale = obj.transform.localScale.x;
            first = false;
            GameInfo.IsSummer.Subscribe(Summer);
            GameInfo.IsWinter.Subscribe(Winter);
            GameInfo.treeGrow.Subscribe(treeGrow);
        }
    }

    private void treeGrow()
    {
        var step = TreeSkript.Instance.Step;
        var size = TreeSkript.Instance.transform.localScale.x;
        var oldSize = size - step;
        var percent = size / oldSize;
        scale = scale / percent;
        if (obj != null)
        {
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private void Winter()
    {
        rotation = obj.transform.rotation;
        var fallPoint = Instantiate(Resources.Load<GameObject>("FallPoint"), transform.position, transform.rotation);
        var target = obj.GetComponent<TargetSkript>();
        fallPoint.GetComponent<Fall>().link = obj;
        target.target = fallPoint;
        obj.transform.SetParent(fallPoint.transform);
    }

    private void Summer()
    {
        obj = Instantiate(prefab);
        obj.GetComponent<SquareController>().enabled = false;
        obj.transform.rotation = rotation;
        obj.AddComponent<GrowList>();
        obj.GetComponent<TargetSkript>().target = gameObject;
        obj.transform.SetParent(transform);
    }
}
