using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(items.Count);
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i < items.Count)
            {
                transform.GetChild(i).GetComponent<InventorySlot>().item = items[i];
                transform.GetChild(i).GetComponent<InventorySlot>().amount = 1;
            }
            else
            {
                transform.GetChild(i).GetComponent<InventorySlot>().item = null;
                transform.GetChild(i).GetComponent<InventorySlot>().amount = 0;
            }
        }
    }
}
