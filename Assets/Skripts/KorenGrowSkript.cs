using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KorenGrowSkript : MonoBehaviour
{
    public float Step;

    private int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int GetLavel() => level;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grow()
    {
        transform.localScale += new Vector3(Step, Step, 0);
        level++;
    }
}
