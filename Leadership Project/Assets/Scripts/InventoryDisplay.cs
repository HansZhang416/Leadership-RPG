using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class InventoryDisplay : MonoBehaviour
{
    // public List<Item> items = new List<Item>();
    SaveLoadManager saveLoadManager;
    // Start is called before the first frame update
    void Start()
    {
        saveLoadManager = Center_Manager.Instance.saveLoadManager;
        // Debug.Log(items.Count);
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventory()
    {
        if (Center_Manager.Instance.saveLoadManager.currentUserData == null || !Center_Manager.Instance.saveLoadManager.currentUserData.ContainsKey("inventory"))
        {
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<InventorySlot>().item = null;
            transform.GetChild(i).GetComponent<InventorySlot>().amount = 0;
        }

        // iterate through the save data and add items to the inventory slots
        foreach (string item in Center_Manager.Instance.saveLoadManager.currentUserData["inventory"] as List<object>)
        {
            Debug.Log(item);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<InventorySlot>().item == null)
                {
                    transform.GetChild(i).GetComponent<InventorySlot>().item = saveLoadManager.itemDict[item];
                    transform.GetChild(i).GetComponent<InventorySlot>().amount = 1;
                    break;
                }
                else if (transform.GetChild(i).GetComponent<InventorySlot>().item.name == item)
                {
                    transform.GetChild(i).GetComponent<InventorySlot>().amount++;
                    transform.GetChild(i).GetComponent<InventorySlot>().UpdateAmount();
                    break;
                }
            }
        }

        // // currency = Int32.Parse($"{Center_Manager.Instance.saveLoadManager.currentUserData["currency"]}");
        // items.Clear();
        // if (Center_Manager.Instance.saveLoadManager.currentUserData.ContainsKey("inventory"))
        // {
        //     foreach (string item in Center_Manager.Instance.saveLoadManager.currentUserData["inventory"] as List<object>)
        //     {
        //         Debug.Log(item);
        //         items.Add(Center_Manager.Instance.saveLoadManager.itemDict[item]);
        //     }
        // }
        

        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     if (i < items.Count)
        //     {
        //         transform.GetChild(i).GetComponent<InventorySlot>().item = items[i];
        //         transform.GetChild(i).GetComponent<InventorySlot>().amount = 1;
        //     }
        //     else
        //     {
        //         transform.GetChild(i).GetComponent<InventorySlot>().item = null;
        //         transform.GetChild(i).GetComponent<InventorySlot>().amount = 0;
        //     }
        // }
    }
}
