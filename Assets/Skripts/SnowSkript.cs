using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSkript : MonoBehaviour
{
    void Start()
    {
        GameInfo.IsSummer.Subscribe(() =>
        {
            gameObject.SetActive(false);
        });
        GameInfo.IsWinter.Subscribe(() =>
        {
            gameObject.SetActive(true);
        });
        gameObject.SetActive(false);
    }

}
