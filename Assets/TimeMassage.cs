using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMassage : MonoBehaviour
{
    public float Speed = 5;
    public float time = 5;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Speed);
    }
}
