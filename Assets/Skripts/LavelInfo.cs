﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavelInfo : MonoBehaviour
{
    //уровень, после которого можно начать строить на данном элементе
    public int BuildLavel = 3;
    public int Lavel { get; private set; }

    public bool BuildLavelConsider = true;

    private void Start()
    {
        Lavel = 1;
    }

    public void Increment()
    {
        Lavel++;
    }

    public void Decrement()
    {
        if (Lavel == 1) return;
        Lavel--;
    }

    public bool GetConfirm()
    {
        if(BuildLavelConsider)
            return Lavel >= BuildLavel;
        return true;
    }
}