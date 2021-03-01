using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpSkript : MonoBehaviour
{
    public List<GameObject> gs;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            gs.ForEach(x => x.SetActive(!x.activeSelf));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
