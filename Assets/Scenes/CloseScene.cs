﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseScene : MonoBehaviour
{
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.UnloadSceneAsync(name);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
