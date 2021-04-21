using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LavelControl : MonoBehaviour
{
    public int Lavel;
    private const string SAVE_LAVEL = "Lavel";
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(SAVE_LAVEL)) 
            PlayerPrefs.SetInt(SAVE_LAVEL, 1);
        var realLavel = PlayerPrefs.GetInt(SAVE_LAVEL);
        if (realLavel < Lavel) GetComponent<Button>().interactable = false;
    }
}
