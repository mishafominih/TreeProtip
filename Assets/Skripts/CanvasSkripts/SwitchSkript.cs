using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSkript : MonoBehaviour
{
    public List<GameObject> Items;
    public bool Direction;
    public List<PrintCast> casts;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            var active = Items.Where(x => x.activeSelf).First();
            var index = Items.IndexOf(active);
            var next = index + (Direction ? 1 : -1);
            if(Items.Count > next && next >= 0)
            {
                Items[next].SetActive(true);
                casts.ForEach(x => x.g = Items[next]);
                active.SetActive(false);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
