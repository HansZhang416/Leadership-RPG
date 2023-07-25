using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using TMPro;
using System;

public class PlayHandler : MonoBehaviour
{
    public TextMeshProUGUI currencyIndicator;
    public GameObject player;
    public GameObject vendorUI;
    public List<Sprite> upgradeSprites;
    [HideInInspector]
    public int currency;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("aaa");
        // if (Center_Manager.Instance != null)
        // {
        //     Debug.Log("Center Manager is not null");
        //     Debug.Log(Center_Manager.Instance.saveLoadManager.currentUserData["currency"]);
        //     currencyIndicator.text = "$" + Center_Manager.Instance.saveLoadManager.currentUserData["currency"];
        // }
        currencyIndicator.text = "$" + Center_Manager.Instance.saveLoadManager.currentUserData["currency"];
        currency = Int32.Parse($"{Center_Manager.Instance.saveLoadManager.currentUserData["currency"]}");
        // Debug.Log(currency);
    }

    public void Equip(string item)
    {
        switch(item)
        {
            case "armor":
                player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = upgradeSprites[0];
                player.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = upgradeSprites[1];
                player.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = upgradeSprites[2];
                player.GetComponent<PlayerCombat>().armorValue = 3;
                Choice();
                break;
            case "sword":
                player.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = upgradeSprites[3];
                player.GetComponent<PlayerCombat>().weaponValue = 2;
                Choice();
                break;
            case "shield":
                player.transform.GetChild(5).GetComponent<SpriteRenderer>().sprite = upgradeSprites[4];
                player.GetComponent<PlayerCombat>().shieldValue = 1;
                Choice();
                break;
        }
    }

    public void Choice()
    {
        player.GetComponent<PlayerMovement>().canMove = true;
        player.GetComponent<PlayerCombat>().canAttack = true;
        vendorUI.SetActive(false);
    }
}
