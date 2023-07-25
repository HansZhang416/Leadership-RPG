using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Managers;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
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

    public void UpdateAmount()
    {
        amountText.text = amount.ToString();
    }

    // check if user right-clicked on me
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log($"Right-clicked on me {this.name}");
            if (item != null)
            {
                Debug.Log($"There is an item in me {item.itemName}");

                // use the item
                item.Use();

                // delete the item from the inventory
                if (item.itemType == Item.ItemType.Consumable)
                {
                    amount--;
                    UpdateAmount();
                    if (amount == 0)
                    {
                        item = null;
                    }
                    Center_Manager.Instance.authManager.DeleteItem(item.name);
                }
                
            }
        }
    }
}
