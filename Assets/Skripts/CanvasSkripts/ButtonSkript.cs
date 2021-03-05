﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkript : MonoBehaviour
{
    public GameObject target;
    public GameObject rotate;
    public GameObject HidesSecond;
    public GameObject HidesFirst;


    GameObject newG;
    int counter = 0;

    void Start()
    {
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

    private void CheckAndSet(string baseNameTag)
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag(baseNameTag))
        {
            if (obj.GetComponent<PolygonCollider2D>().OverlapPoint(newG.transform.position) && obj != newG)
            {
                var tt = Instantiate(target, newG.transform.position, new Quaternion());
                tt.transform.SetParent(obj.transform.GetChild(0).transform);
                newG.GetComponent<TargetSkript>().target = tt;
                rotate.SetActive(false);
                if (newG.tag == "tree")
                {
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
        newG = Instantiate(g, new Vector3(0, 0, 0), new Quaternion());
        rotate.GetComponent<joysticController>().sqController = newG.GetComponent<SquareController>();
    }

    private void Update()
    {
        var image = GetComponent<Image>();
        image.color = Color.white;
        if (counter != 0 && newG != null)
        {
            var count = GameObject.FindGameObjectsWithTag((newG.tag == "root") ? "root" : "tree")
                .Where(tree => tree != newG)
                .Where(tree => tree.GetComponent<PolygonCollider2D>().OverlapPoint(newG.transform.position))
                .Count();
            if(count > 0)
            {
                image.color = Color.green;
            }
        }
    }
}
