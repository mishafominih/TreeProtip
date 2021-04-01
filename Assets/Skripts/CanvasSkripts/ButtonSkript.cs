using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkript : MonoBehaviour
{
    public GameObject target;
    public GameObject rotate;
    public GameObject revert;
    public GameObject HidesSecond;
    public GameObject HidesFirst;


    GameObject newG;
    int counter = 0;

    void Start()
    {
        GameInfo.IsWinter.Subscribe(() => Null());
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (counter == 0)
            {
                Do(true);
                counter++;
            }
            else
            {
                if (newG == null)
                {
                    Do(false);
                    counter = 0;
                    return;
                }

                
                if (newG.tag == "root") CheckAndSet("root");
                else CheckAndSet("tree");

                return;
            }
        });
    }

    public void Null()
    {
        Destroy(newG);
        newG = null;
        rotate.SetActive(false);
        revert.SetActive(false);
    }

    private void CheckAndSet(string baseNameTag)
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag(baseNameTag))
        {
            if (newG.tag == "list" || obj.GetComponent<LavelInfo>().GetConfirm())
                if (obj.GetComponent<PolygonCollider2D>().OverlapPoint(newG.transform.position) && obj != newG)
                {
                    var tt = Instantiate(target, newG.transform.position, new Quaternion());
                    tt.transform.SetParent(obj.transform);
                    newG.transform.SetParent(tt.transform);
                    newG.GetComponent<TargetSkript>().SetTarget(tt);
                    rotate.SetActive(false);
                    revert.SetActive(false);
                    if (newG.tag == "tree")
                    {
                        newG.GetComponent<PartTreeGrow>().enabled = true;
                        foreach (var g in GameObject.FindGameObjectsWithTag("stock"))
                        {
                            g.GetComponent<Stock>().AddStart();
                        }
                    }
                    if(newG.tag == "list")
                        newG.GetComponent<SquareController>().enabled = false;
                    counter = 1;
                    newG = null;
                    break;
                }
        }
    }

    private void Do(bool bb)
    {
        HidesSecond.SetActive(!bb);
        HidesFirst.SetActive(bb);
    }

    public void Click(GameObject g)
    {
        //Do(false);
        rotate.SetActive(true);
        revert.SetActive(true);
        newG = Instantiate(g, new Vector3(0, 0, 0), new Quaternion());
        rotate.GetComponent<joysticController>().sqController = newG.GetComponent<SquareController>();
    }

    public bool IsFree() => newG == null;

    private void Update()
    {
        var image = GetComponent<Image>();
        image.color = Color.white;
        if (counter != 0 && newG != null)
        {
            var count = GameObject.FindGameObjectsWithTag((newG.tag == "root") ? "root" : "tree")
                .Where(tree => tree != newG)
                .Where(tree => (newG.tag == "list" ? true : tree.GetComponent<LavelInfo>().GetConfirm()))
                .Where(tree => tree.GetComponent<PolygonCollider2D>().OverlapPoint(newG.transform.position))
                .Count();
            if (count > 0)
            {
                image.color = Color.green;
            }
        }
    }
}
