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
