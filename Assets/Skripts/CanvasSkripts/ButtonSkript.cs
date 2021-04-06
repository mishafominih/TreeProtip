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
    public List<GameObject> Hides;
    public List<GameObject> Actives;


    GameObject newG;

    void Start()
    {
        GameInfo.IsWinter.Subscribe(() => Null());
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (newG.tag == "root") CheckAndSet("root");
            else CheckAndSet("tree");

            return;
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
                    hide();
                    if (newG.tag == "tree")
                    {
                        newG.GetComponent<PartTreeGrow>().enabled = true;
                        foreach (var g in GameObject.FindGameObjectsWithTag("stock"))
                        {
                            g.GetComponent<Stock>().AddStart();
                        }
                    }
                    newG.GetComponent<SquareController>().enabled = false;
                    newG = null;
                    break;
                }
        }
    }

    private void hide()
    {
        rotate.SetActive(false);
        revert.SetActive(false);
        gameObject.SetActive(false);
        foreach (var g in Hides) g.SetActive(false);
        foreach (var g in Actives) g.SetActive(true);
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
        if (newG != null)
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
