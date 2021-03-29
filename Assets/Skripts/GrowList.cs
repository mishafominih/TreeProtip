using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowList : MonoBehaviour
{
    public float speedMin = 0.0005f;
    public float speedMax = 0.005f;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3();
        speed = Random.Range(speedMin, speedMax);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < 1)
        {
            transform.localScale += new Vector3(speed, speed, speed);
        }
        else
        {
            enabled = false;
        }
    }
}
