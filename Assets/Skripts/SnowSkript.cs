using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSkript : MonoBehaviourSave
{
    public override string SaveData()
    {
        return gameObject.activeSelf.ToString();
    }

    public override void LoadData(string data)
    {
        gameObject.SetActive(bool.Parse(data));
    }

    private void OnDisable()
    {
        this.enabled = true;
    }

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
