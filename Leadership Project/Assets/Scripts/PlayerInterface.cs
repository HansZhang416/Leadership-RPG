using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    public GameObject inventoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
    }
}
