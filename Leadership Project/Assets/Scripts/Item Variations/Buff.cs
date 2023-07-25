using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Items/Buff")]
public class Buff : Item
{
    public enum BuffTypes { Attack, Regeneration, Invincibility }
    public BuffTypes buffType;

    public override void Use()
    {
        PlayerCombat playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        switch (buffType)
        {
            case BuffTypes.Attack:
                Debug.Log("Used Attack Buff!");
                // raise the player's attack by 2 for 1
                playerCombat.BuffInterface("attack");
                break;
            case BuffTypes.Regeneration:
                Debug.Log("Used Regeneration Buff!");
                playerCombat.BuffInterface("regeneration");
                break;
            case BuffTypes.Invincibility:
                Debug.Log("Used Invincibility Buff!");
                playerCombat.BuffInterface("invincibility");
                break;
            default:
                Debug.Log("Used Buff!");
                break;
        }
    }
}