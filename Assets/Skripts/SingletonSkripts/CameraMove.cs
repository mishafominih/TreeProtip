using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove Instance;
    public float speedDistance = 0.5f;
    public float speedMove = 0.5f;
    public float ControlX = 3;
    public float UpY = 3;
    public float ControlSize = 8;
    public GameObject Joistick;
    public float MidleY = -7;
    public float DownY = -7;

    private Camera cam;
    private Vector2 PreviousTouch;
    private bool firstTouch = true;
    private float previousDist;
    private bool firstDist  = true;
    private bool isUp = true;
    private bool isAnimation = false;
    //private float distance;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimation)
            LogicControl();
        else
        {
            UpdatePosition();//изменяет позицию
            UpdateSize();//изменяет массштаб
            if (isUp)
                Control(UpY, 0);//контроллирует нахождение камеры в указанных пределах
            else
            {
                Control(MidleY, DownY);//контроллирует нахождение камеры в указанных пределах
                if (cam.orthographicSize > 5) cam.orthographicSize = 5;
            }
        }
    }

    private void LogicControl()
    {
        if (isUp)
            if (transform.position.y < 0)
                ControlAnim(0.5f);
            else
                isAnimation = false;
        else
            if (transform.position.y > MidleY)
            ControlAnim(-0.5f);
        else
            isAnimation = false;
    }

    private void ControlAnim(float d)
    {
        if (transform.position.x != 0)
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        if (Mathf.Abs(cam.orthographicSize - 5) < 0.15f) cam.orthographicSize = 5;
        if (cam.orthographicSize < 5) cam.orthographicSize += 0.1f;
        else if (cam.orthographicSize > 5) cam.orthographicSize -= 0.1f;
        else transform.position += new Vector3(0, d, 0);
    }

    public void Switch()
    {
        isUp = !isUp;
        isAnimation = true;
    }

    private void Control(float upY, float downY)
    {
        if (cam.orthographicSize - 5 > transform.position.y - downY)
        {
            transform.position = new Vector3(
                transform.position.x,
                cam.orthographicSize - 5 + downY,
                transform.position.z
                );
        }
        if (cam.orthographicSize > ControlSize)
            cam.orthographicSize = ControlSize;
        if (cam.orthographicSize < 0.1f)
            cam.orthographicSize = 0.1f;
        float vect = upY - (cam.orthographicSize - 5) / 2;
        if (vect < transform.position.y)
        {
            transform.position = new Vector3(
                transform.position.x,
                vect,
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
        if (Input.touchCount == 1 || Input.GetMouseButton(0))
        {
            var touch = GetTouch();
            
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
        else if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float mw = -Input.GetAxis("Mouse ScrollWheel");
            cam.orthographicSize += mw;
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + mw,
                transform.position.z);
        }
        else 
            firstDist = true;
    }

    private bool Check(Vector2 touch)
    {
        return !Joistick.activeSelf;
    }


    private static Vector2 GetTouch()
    {
        if (Input.touchCount > 0) return Input.GetTouch(0).position;
        return Input.mousePosition;
    }
}
