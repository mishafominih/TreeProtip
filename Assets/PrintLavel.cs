using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintLavel : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = TreeSkript.Instance.GetComponent<LavelInfo>().Lavel.ToString();
    }
}
