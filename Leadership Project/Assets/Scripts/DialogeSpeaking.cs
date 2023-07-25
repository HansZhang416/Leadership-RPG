using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeSpeaking : MonoBehaviour
{
    public GameObject indicator;
    public List<GameObject> conversations;
    public int selectedConvo = 0;
    public int currentLine = 0;
    public int audienceSentiment = 0;
    bool playerInRange;
    [HideInInspector] public GameObject player;
    
    void PickConversation() {
        selectedConvo = Random.Range(0,conversations.Count - 1);
        Debug.Log($"The selected convo should be: {selectedConvo}");
        for (int i = 0; i < conversations.Count; i++) {
            if (i == selectedConvo) {
                conversations[i].SetActive(true);
            } else {
                conversations[i].SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // PickConversation();
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
                // responseText.text = "";
                // if (dialogueType == DialogueType.General)
                // {
                //     generalDialogue.SetActive(true);
                // }
                // else if (dialogueType == DialogueType.Quiz)
                // {
                //     quizDialogue.SetActive(true);
                // }
                player.GetComponent<PlayerMovement>().canMove = false;
                player.GetComponent<PlayerCombat>().canAttack = false;
                PickConversation();
            }
            player.GetComponent<PlayerInterface>().HideInventory();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has entered the trigger");
            // PickConversation();
            indicator.SetActive(true);
            // responseText.text = "";
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
