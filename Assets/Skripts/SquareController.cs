using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    public float Angle;

    // Start is called before the first frame update
    void Start()
    {
        Angle = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Angle);
    }
}
