using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CharacterDisplay : MonoBehaviour
{
    public string characterString = "0 0 0 -1 -1 -1 -1 -1";
    string currentString;
    public Character characterStorage;
    public Image body;
    public Image shirt;
    public Image pants;
    public Image shoes;
    public Image hair;
    public Image hats;
    public Image weapon;
    public Image shield;

    void Start()
    {
        currentString = characterString;
        // string[] loadout = characterString.Split(' ');

        // foreach (var item in loadout)
        // {
        //     Debug.Log($"This is a test: {Int32.Parse(item)}");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterString.Equals(currentString))
        {
            // Debug.Log("Updating Character Image");

            string[] loadout = characterString.Split(' ');
            int[] loadoutIdx = new int[8];

            for (int i = 0; i < 8; i++)
            {
                loadoutIdx[i] = Int32.Parse(loadout[i]);
            }

            body.sprite = characterStorage.skintones[loadoutIdx[0]];
            shirt.sprite = characterStorage.shirt[loadoutIdx[1]];
            pants.sprite = characterStorage.pants[loadoutIdx[2]];

            if (loadoutIdx[3] >= 0)
            {
                shoes.transform.gameObject.SetActive(true);
                shoes.sprite = characterStorage.shoes[loadoutIdx[3]];
            }
            else
            {
                shoes.transform.gameObject.SetActive(false);
            }

            if (loadoutIdx[4] >= 0)
            {
                hair.transform.gameObject.SetActive(true);
                hair.sprite = characterStorage.hair[loadoutIdx[4]];
            }
            else
            {
                hair.transform.gameObject.SetActive(false);
            }

            if (loadoutIdx[5] >= 0)
            {
                hats.transform.gameObject.SetActive(true);
                hats.sprite = characterStorage.hats[loadoutIdx[5]];
            }
            else
            {
                hats.transform.gameObject.SetActive(false);
            }

            if (loadoutIdx[6] >= 0)
            {
                weapon.transform.gameObject.SetActive(true);
                weapon.sprite = characterStorage.weapon[loadoutIdx[6]];
            }
            else
            {
                weapon.transform.gameObject.SetActive(false);
            }

            if (loadoutIdx[7] >= 0)
            {
                shield.transform.gameObject.SetActive(true);
                shield.sprite = characterStorage.shield[loadoutIdx[7]];
            }
            else
            {
                shield.transform.gameObject.SetActive(false);
            }



            currentString = characterString;
        }
    }
}
