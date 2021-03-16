using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropSkript : MonoBehaviour
{
    private int lavel = -1;
    private List<GameObject> objects = new List<GameObject>();

    public float Sugar;
    public float Water;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objects.Count != 0) 
        {
            if (objects.Count == 2 || 
                objects.First().GetComponent<LavelInfo>().Lavel - lavel > 0)
            {
                var sugar = GameInfo.Instance.sugar;
                var water = GameInfo.Instance.water;
                sugar.AddPart(Sugar);
                water.AddPart(Water);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "root")
        {
            if (!objects.Contains(collision.gameObject) &&
                collision.GetComponent<TargetSkript>().target != null)
            {
                objects.Add(collision.gameObject);
                if(objects.Count == 1)
                    lavel = collision.GetComponent<LavelInfo>().Lavel;
            }
        }

    }
}
