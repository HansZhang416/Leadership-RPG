using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Conversation : MonoBehaviour
{
    [TextArea(3, 10)]
    public string startingLine;

    [TextArea(3, 10)]
    public List<string> followupLines;

    public List<int> lineSentiment;

    public Transform choices;

    [Header("UI")]
    public TextMeshProUGUI startingText;
    // public TextMeshProUGUI rewardText;

    // Start is called before the first frame update
    void Start()
    {
        startingText.text = startingLine;
        for (int i = 0; i < 5; i++) 
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
        
    }
}
