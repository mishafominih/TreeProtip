using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeSkript : MonoBehaviour
{
    public static TreeSkript Instance { get; private set; }


    public float Step;
    private LavelInfo lavel;

    public void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        lavel = GetComponent<LavelInfo>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns> возвращает вначале глюкозу, потом воду
    public List<Stock> GetStocks()
    {
        var stocks = GetComponentsInChildren<Stock>();
        return stocks.OrderByDescending(x => x.MaxValue).ToList();
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
        lavel.Increment();
        foreach (var g in GameObject.FindGameObjectsWithTag("stock"))
        {
            g.GetComponent<Stock>().MaxValue *= 1.2f;
            lavel.Increment();
        }
    }
}
