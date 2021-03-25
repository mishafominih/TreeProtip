
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Seazons : MonoBehaviour
{
    public static Seazons Instance { get; private set; }
    public bool IsSummer { get; private set; }

    public float SummerTime;
    public Sprite SummerBackGround;
    public Sprite SummerBackGroundDown;

    public float WinterTime;
    public Sprite WinterBackGround;
    public Sprite WinterBackGroundDown;

    private SpriteRenderer realBackGround;
    private SpriteRenderer realBackGroundDown;
    private float timer = 0;

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        IsSummer = true;
        realBackGround = transform.GetComponentsInChildren<SpriteRenderer>().
            Where(x => x.gameObject.name == "UpBackGround").FirstOrDefault();
        realBackGroundDown = transform.GetComponentsInChildren<SpriteRenderer>().
            Where(x => x.gameObject.name == "DownBackGround").FirstOrDefault();
        realBackGround.sprite = SummerBackGround;
        realBackGroundDown.sprite = SummerBackGroundDown;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!IsSummer)
            CheckTime(SummerTime, SummerBackGround, SummerBackGroundDown);
        else
            CheckTime(WinterTime, WinterBackGround, WinterBackGroundDown);
    }

    private void CheckTime(float SeazonTime, Sprite BackGround, Sprite BackGraundDown)
    {
        if(timer >= SeazonTime)
        {
            realBackGround.sprite = BackGround;
            realBackGroundDown.sprite = BackGraundDown;
            IsSummer = !IsSummer;
            timer = 0;
            if (IsSummer)
                GameInfo.IsSummer.Publish();
            else
                GameInfo.IsWinter.Publish();

        }
    }
}
