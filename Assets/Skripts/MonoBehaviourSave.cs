using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSave : MonoBehaviour
{
    public virtual string SaveData()
    {
        return "";
    }

    public virtual void LoadData(string data)
    {

    }
}
