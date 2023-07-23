using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using TMPro;
using System;

public class PlayHandler : MonoBehaviour
{
    public TextMeshProUGUI currencyIndicator;
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
}
