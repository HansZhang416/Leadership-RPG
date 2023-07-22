using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;

public class Purchase : MonoBehaviour
{
    public GameObject indicator;
    public Item item;
    bool playerInRange;
    public PlayHandler playHandler;
    // public TextMeshProUGUI responseText;
    // Start is called before the first frame update
    void Start()
    {
        playHandler = GameObject.Find("Player").GetComponent<PlayHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player has pressed E");
                indicator.SetActive(false);

                // purchase the item and add it to the inventory and to firestore
                if (playHandler.currency >= item.value)
                {
                    playHandler.currency -= item.value;
                    Center_Manager.Instance.saveLoadManager.currentUserData["currency"] = playHandler.currency;
                    // Center_Manager.Instance.saveLoadManager.currentUserData["inventory"].Add(item.name);
                    // Center_Manager.Instance.saveLoadManager.SaveUserData();

                    // add item to inventory
                    Center_Manager.Instance.authManager.AddItem(item.name);

                    Debug.Log("Item purchased");

                    // destroy the item
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Not enough money");
                }
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has entered the trigger");
            indicator.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has exited the trigger");
            indicator.SetActive(false);
            playerInRange = false;
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject indicator;
    public TextMeshProUGUI responseText;
    public enum DialogueType { General, Quiz };
    public DialogueType dialogueType;
    public GameObject generalDialogue;
    public GameObject quizDialogue;

    public Transform followupQuestions;

    [HideInInspector] public GameObject player;
    bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player has pressed E");
                indicator.SetActive(false);
                responseText.text = "";
                if (dialogueType == DialogueType.General)
                {
                    generalDialogue.SetActive(true);
                }
                else if (dialogueType == DialogueType.Quiz)
                {
                    quizDialogue.SetActive(true);
                }
                player.GetComponent<PlayerMovement>().canMove = false;
                player.GetComponent<PlayerCombat>().canAttack = false;
            }

            player.GetComponent<PlayerInterface>().HideInventory();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has entered the trigger");
            indicator.SetActive(true);
            responseText.text = "";
            playerInRange = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has exited the trigger");
            indicator.SetActive(false);
            playerInRange = false;
            player = null;
        }
    }
}

*/