﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSkript : MonoBehaviour
{
    public static RainSkript Instance;

    public GameObject PrefabRain;
    public float Probability = 0.2f;

    private GameObject realRain;
    private float timer;

    private void Awake()
    {
        Instance = this;
        GameInfo.RegisterSaveEvents(stream =>
        {
            stream.Append(Probability);
            stream.Append(" ");
            stream.Append(IsRain());
        }, data =>
        {
            var info = data.Split(' ');
            Probability = float.Parse(info[0]);
            if (bool.Parse(info[1]))
            {
                CreateRain();
            }
        });
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Seazons.Instance.IsSummer)
        {
            if (realRain != null) return;
            if (timer < 5)
                timer += Time.deltaTime;
            else if (Random.Range(Probability - 0.1f, 1) < Probability)
            {
                timer = 0;
                CreateRain();
            }
            else
            {
                timer = 0;
            }
        }
        else
        {
            if(realRain != null)
                Destroy(realRain);
        }
    }

    private void CreateRain()
    {
        realRain = Instantiate(PrefabRain, transform);

        var dur = realRain.GetComponent<ParticleSystem>().duration;
        Destroy(realRain, dur);
    }

    public bool IsRain() => realRain != null;
}
