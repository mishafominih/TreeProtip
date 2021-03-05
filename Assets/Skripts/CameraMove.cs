using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speedDistance = 0.5f;
    public float speedMove = 0.5f;
    public float ControlX = 3;
    public float ControlY = 3;
    public float ControlSize = 8;
    public GameObject Joistick;
    public float DownLayer = -7;

    private Camera cam;
    private Vector2 PreviousTouch;
    private bool firstTouch = true;
    private float previousDist;
    private bool firstDist  = true;
    private bool isUp = true;
    //private float distance;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUp)
        {
            if (transform.position.y < 0 && cam.orthographicSize == 5)
                transform.position += new Vector3(0, 0.1f, 0);
            else
            {
                UpdatePosition();//изменяет позицию
                UpdateSize();//изменяет массштаб
                Control();//контроллирует нахождение камеры в указанных пределах
            }
        }
        else
        {
            if (transform.position.y > DownLayer)
            {
                if (transform.position.x != 0) 
                    transform.position = new Vector3(0, transform.position.y, transform.position.z);
                if (Mathf.Abs(cam.orthographicSize - 5) < 0.15f) cam.orthographicSize = 5;
                if (cam.orthographicSize < 5) cam.orthographicSize += 0.1f;
                else if (cam.orthographicSize > 5) cam.orthographicSize -= 0.1f;
                else transform.position += new Vector3(0, -0.1f, 0);
            }
        }
    }

    public void Switch()
    {
        isUp = !isUp;
    }

    private void Control()
    {
        if (cam.orthographicSize - 5 > transform.position.y)
        {
            transform.position = new Vector3(
                transform.position.x,
                cam.orthographicSize - 5,
                transform.position.z
                );
        }
        if (cam.orthographicSize > ControlSize)
            cam.orthographicSize = ControlSize;
        if(transform.position.y > ControlY)
        {
            transform.position = new Vector3(
                transform.position.x,
                ControlY,
                transform.position.z
                );
        }
        if (Mathf.Abs(transform.position.x) > ControlX)
        {
            transform.position = new Vector3(
                (transform.position.x > ControlX) ? ControlX : -ControlX,
                transform.position.y,
                transform.position.z
                );
        }
    }

    private void UpdatePosition()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0).position;
            
            if (Check(touch))
            {
                if (firstTouch)
                {
                    PreviousTouch = touch;
                    firstTouch = false;
                }
                var d = (PreviousTouch - touch) * speedMove;
                transform.position = new Vector3(
                    transform.position.x + d.x,
                    transform.position.y + d.y,
                    transform.position.z);
                PreviousTouch = touch;
            }
        }
        else
            firstTouch = true;
    }

    private void UpdateSize()
    {
        if (Input.touchCount == 2)
        {
            var dist = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            dist = Mathf.Abs(dist);
            if (firstDist)
            {
                previousDist = dist;
                firstDist = false;
            }
            var d = (previousDist - dist) * speedDistance;
            cam.orthographicSize += d;
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + d,
                transform.position.z);
            previousDist = dist;
        }
        else 
            firstDist = true;
    }

    private bool Check(Vector2 touch)
    {
        return !Joistick.activeSelf;
    }
}
