using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeSkript : MonoBehaviour
{
    public float Step;
    private int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerable<float> GetCast()
    {
        foreach (var f in GameObject.FindGameObjectsWithTag("stock")
            .Select(x => x.GetComponent<Stock>().MaxValue)
            .OrderByDescending(x => x).ToList())
        {
            yield return f * 0.66f;
        }
    }

    public void Grow()
    {
        transform.localScale = new Vector3(
            transform.localScale.x + Step,
            transform.localScale.y + Step);
        level++;
        foreach (var g in GameObject.FindGameObjectsWithTag("stock"))
        {
            g.GetComponent<Stock>().MaxValue *= 1.2f;
        }
    }
}
