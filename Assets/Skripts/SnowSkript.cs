using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSkript : MonoBehaviour
{
    private void Awake()
    {
    }

    private void OnDisable()
    {
        this.enabled = true;
    }

    void Start()
    {
        GameInfo.RegisterSaveEvents(stream =>
        {
            stream.Append(gameObject.activeSelf);
        }, data =>
        {
            gameObject.SetActive(bool.Parse(data));
        });
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
