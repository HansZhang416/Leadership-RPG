using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    Item lastItem;
    public Item item;
    public int amount;
    public Image icon;
    public TextMeshProUGUI amountText;
    public GameObject itemInfoPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((item != null) && (item != lastItem))
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            amountText.text = amount.ToString();
            amountText.enabled = true;
            lastItem = item;

            itemInfoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.description;
        }
        else if (item == null)
        {
            icon.sprite = null;
            icon.enabled = false;
            amountText.text = "";
            amountText.enabled = false;

            itemInfoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
