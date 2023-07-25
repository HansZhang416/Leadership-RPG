using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFood", menuName = "Items/Food")]
public class Food : Item
{
    public int healAmount;
    public bool regeneration;
    public float interval;
    public int limit;

    public override void Use()
    {
        Debug.Log("Used Potion! Healed for " + healAmount);
        // Implement your healing logic here

        if (regeneration)
        {
            Debug.Log("Regeneration enabled");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().Heal(healAmount, true, interval, limit);
        }
        else
        {
            Debug.Log("Regeneration disabled");

            // find the player based on tag and apply the heal amount
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().Heal(healAmount);
        }
    }
}