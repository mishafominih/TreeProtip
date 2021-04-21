using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class LavelInfo : MonoBehaviour
{
    //уровень, после которого можно начать строить на данном элементе
    public int BuildLavel = 3;
    public int Lavel = 1;

    public bool BuildLavelConsider = true;

    private void Start()
    {
        GameInfo.RegisterSaveEvents(
        stream =>
        {
            stream.Append(Lavel);
            stream.Append(" ");
            stream.Append(BuildLavel);
            stream.Append(" ");
            stream.Append(BuildLavelConsider);
        },
        data =>
        {
            var info = data.Split(' ');
            Lavel = int.Parse(info[0]);
            BuildLavel = int.Parse(info[1]);
            BuildLavelConsider = bool.Parse(info[2]);
        });
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
