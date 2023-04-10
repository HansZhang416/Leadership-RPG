using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    [TextArea(3, 10)]
    public List<string> dialogLines = new List<string>();
    public TextMeshProUGUI dialogText;
    public GameObject speaker;
    public Image speakerImage;
    int currentLine;
    // Start is called before the first frame update
    void Start()
    {
        speakerImage.sprite = speaker.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // detect left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // if there are still lines of dialog left
            if (dialogLines.Count > currentLine)
            {
                currentLine++;
            }
            else
            {
                // if there are no more lines of dialog, disable this object
                currentLine = 0;
                gameObject.SetActive(false);
                speaker.GetComponent<NPC>().player.GetComponent<PlayerMovement>().canMove = true;
                speaker.GetComponent<NPC>().player.GetComponent<PlayerCombat>().canAttack = true;
            }
        }

        // if there are still lines of dialog left
        if (dialogLines.Count > currentLine)
        {
            // display the first line of dialog
            dialogText.text = dialogLines[currentLine];
        }
        else
        {
            // if there are no more lines of dialog, disable this object
            currentLine = 0;
            gameObject.SetActive(false);
            speaker.GetComponent<NPC>().player.GetComponent<PlayerMovement>().canMove = true;
            speaker.GetComponent<NPC>().player.GetComponent<PlayerCombat>().canAttack = true;
        }
    }
}
