using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowList : MonoBehaviour
{
    public float MaxScale;
    public float speedMin = 0.1f;
    public float speedMax = 1f;

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
        MaxScale = GetComponentInParent<Link>().GetScale();
        if(transform.localScale.x < MaxScale)
        {
            var delta = speed * Time.deltaTime;
            transform.localScale += new Vector3(delta, delta, delta);
        }
        else
        {
            enabled = false;
        }
    }
}
