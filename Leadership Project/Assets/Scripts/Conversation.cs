using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using TMPro;

public class Conversation : MonoBehaviour
{
    public DialogeSpeaking container;
    public bool isLastConvoLine;
    [TextArea(3, 10)]
    public string startingLine;

    [TextArea(3, 10)]
    public List<string> followupLines;

    public List<int> lineSentiment;

    public Transform choices;
    public GameObject reactionParticles;

    [Header("UI")]
    public TextMeshProUGUI startingText;
    // public TextMeshProUGUI rewardText;

    // Start is called before the first frame update
    void Start()
    {
        startingText.text = startingLine;
        for (int i = 0; i < 4; i++) 
        {
            if (i < followupLines.Count) 
            {
                choices.GetChild(i).gameObject.SetActive(true);
                choices.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = followupLines[i];
            }
            else
            {
                choices.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void Choice(int choiceIdx)
    {
        container.audienceSentiment += lineSentiment[choiceIdx];
        Debug.Log($"Audience sentiment changed by {lineSentiment[choiceIdx]}");
        container.currentLine++;

        foreach (Transform child in reactionParticles.transform) {
            child.gameObject.SetActive(false);
        }

        if (lineSentiment[choiceIdx] > 0) {
            reactionParticles.transform.GetChild(0).gameObject.SetActive(true);
        } else if (lineSentiment[choiceIdx] < 0) {
            reactionParticles.transform.GetChild(2).gameObject.SetActive(true);
        } else {
            reactionParticles.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (!isLastConvoLine) {
            // enable next question
            container.transform.GetChild(container.selectedConvo + 1).GetChild(container.currentLine).gameObject.SetActive(true);

            // disable self
            gameObject.SetActive(false);
        } 
        else 
        {
            container.player.GetComponent<PlayerMovement>().canMove = true;
            container.player.GetComponent<PlayerCombat>().canAttack = true;
            gameObject.SetActive(false);

            // give currency scaled off of sentiment score
            Debug.Log($"The reward should be {container.audienceSentiment * 50}");
            Center_Manager.Instance.authManager.AddCurrency(container.audienceSentiment * 50);
        }
    }
}
