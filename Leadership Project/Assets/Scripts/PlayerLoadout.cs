using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadout : MonoBehaviour
{
    // public GameObject helmet;
    // public GameObject chest;
    // public GameObject legs;
    // public GameObject weapon;
    // public GameObject shield;

    public List<Sprite> defaultLoadout;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < defaultLoadout.Count; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = defaultLoadout[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
