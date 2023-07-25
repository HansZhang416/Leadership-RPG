using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Shield,
        Armor,
        Consumable,
        Quest,
        Key,
        Misc,
    }

    public ItemType itemType;
    public Sprite icon;
    public GameObject prefab;
    public int value;
    public string itemName;
    [TextArea(3, 10)]
    public string description;

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }
}