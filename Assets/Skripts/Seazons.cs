
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

    public float WinterTime;
    public Sprite WinterBackGround;

    private SpriteRenderer realBackGround;
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
        realBackGround.sprite = SummerBackGround;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!IsSummer)
            CheckTime(SummerTime, SummerBackGround);
        else
            CheckTime(WinterTime, WinterBackGround);
    }

    private void CheckTime(float SeazonTime, Sprite BackGround)
    {
        if(timer >= SeazonTime)
        {
            realBackGround.sprite = BackGround;
            IsSummer = !IsSummer;
            timer = 0;
        }
    }
}
