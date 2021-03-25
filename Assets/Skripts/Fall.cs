using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public GameObject link;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > -5)
            transform.position -= new Vector3(0, 0.01f, 0);
        var min = 5; var max = 10;
        Destroy(gameObject, Random.Range(min, max));
        Destroy(link, Random.Range(min, max));
    }
}
