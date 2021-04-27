using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "root")
        {
            collision.gameObject.GetComponent<Root>().AddWater();
        }
    }
}
