using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop1 : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "root")
        {
            Destroy(collision.transform.parent.gameObject);
            Destroy(collision.gameObject);
        }

    }
}
