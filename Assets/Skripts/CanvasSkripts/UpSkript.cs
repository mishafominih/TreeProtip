using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpSkript : MonoBehaviour
{
    public List<GameObject> gs;

    private List<bool> memory;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if(memory == null)
            {
                memory = new List<bool>(gs.Select(x => x.activeSelf));
                gs.ForEach(x => x.SetActive(false));
            }
            else
            {
                for(int i = 0; i < gs.Count; i++)
                    gs[i].SetActive(memory[i]);
                memory = null;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
