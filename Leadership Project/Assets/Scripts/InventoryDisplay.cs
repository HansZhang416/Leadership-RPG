using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

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
        // currency = Int32.Parse($"{Center_Manager.Instance.saveLoadManager.currentUserData["currency"]}");
        items.Clear();
        if (Center_Manager.Instance.saveLoadManager.currentUserData.ContainsKey("inventory"))
        {
            // foreach (var item in Center_Manager.Instance.saveLoadManager.currentUserData["inventory"]) {
            //     Debug.Log($"Adding {item}");
            //     // items.Add();
            // }
        }
        

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
