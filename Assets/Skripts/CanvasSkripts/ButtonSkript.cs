using System.Collections;
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
                foreach (var tree in GameObject.FindGameObjectsWithTag("tree"))
                {
                    if (tree.GetComponent<BoxCollider2D>().bounds.Contains(newG.transform.position) && tree != newG)
                    {
                        var tt = Instantiate(target, newG.transform.position, new Quaternion());
                        tt.transform.SetParent(tree.transform.GetChild(0).transform);
                        newG.GetComponent<TargetSkript>().target = tt;
                        rotate.SetActive(false);
                        if (newG.tag == "tree")
                        {
                            foreach (var g in GameObject.FindGameObjectsWithTag("stock"))
                            {
                                g.GetComponent<Stock>().AddStart();
                            }
                        }
                        counter = 1;
                        newG = null;
                        return;
                    }
                }
            }
        });
    }

    private void Do(bool bb)
    {
        HidesSecond.SetActive(!bb);
        HidesFirst.SetActive(bb);
    }

    public void Click(GameObject g, Touch t)
    {
        //Do(false);
        rotate.SetActive(true);
        newG = Instantiate(g, new Vector3(0, 0, 0), new Quaternion());
        rotate.GetComponent<joysticController>().sqController = newG.GetComponent<SquareController>();
    }

    private void Update()
    {
        GetComponent<Image>().color = Color.white;
        if (counter != 0 && newG != null)
        {
            var count = GameObject.FindGameObjectsWithTag("tree")
                .Where(tree => tree.GetComponent<BoxCollider2D>().bounds.Contains(newG.transform.position))
                .Count();
            if(count > 0)
            {
                GetComponent<Image>().color = Color.green;
            }
        }
    }
}
