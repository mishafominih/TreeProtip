using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class joysticController : MonoBehaviour
{
    public GameObject touchMarker;
    public SquareController sqController;

    private Vector3 baseVector = new Vector3(10, 0, 0);
    private float first;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        touchMarker.transform.position = transform.position;
        first = touchMarker.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Vector3 touchPos = Input.GetTouch(0).position;
            target = touchPos - transform.position;

            if(target.magnitude > 25 && 
                GetComponent<BoxCollider2D>().bounds.Contains(Input.GetTouch(0).position))
            {
                touchMarker.transform.position = touchPos;

                var a = target.magnitude;
                var b = baseVector.magnitude;
                var c = (target - baseVector).magnitude;
                sqController.Angle = Mathf.Acos(((a * a + b * b - c * c) / (2 * a * b))) / Mathf.PI * 180;
                sqController.Angle = touchMarker.transform.position.y < first ? -sqController.Angle : sqController.Angle;
            }
        }
        else
        {
            touchMarker.transform.position = transform.position;
            //sqController.Angle = 0;
        }
    }
}
