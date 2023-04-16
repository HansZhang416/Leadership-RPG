using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using TMPro;

public class DialogQuiz : MonoBehaviour
{
    public GameObject speaker;
    [Header("Quiz Content")]
    public int reward;

    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse,
    }

    public QuestionType questionType;

    [TextArea(3, 10)]
    public string question;
    public int answer;
    [TextArea(3, 10)]
    public List<string> answers;
    public Transform choices;

    [Header("Quiz UI")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI rewardText;

    // Start is called before the first frame update
    void Start()
    {
        questionText.text = question;
        for (int i = 0; i < 4; i++)
        {
            if (i < answers.Count)
            {
                choices.GetChild(i).gameObject.SetActive(true);
                choices.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = answers[i];
            }
            else
            {
                choices.GetChild(i).gameObject.SetActive(false);
            }
            
            // if (questionType == QuestionType.TrueFalse)
            // {
            //     if (i == 0)
            //     {
            //         choices.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "T";
            //     }
            //     else if (i == 1)
            //     {
            //         choices.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "F";
            //     }
            // }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Choice(int choiceIdx)
    {
        if (choiceIdx == answer)
        {
            Debug.Log("Correct!");
            speaker.GetComponent<NPC>().player.GetComponent<PlayerMovement>().canMove = true;
            speaker.GetComponent<NPC>().player.GetComponent<PlayerCombat>().canAttack = true;
            speaker.GetComponent<NPC>().responseText.text = "Correct!";
            if (Center_Manager.Instance != null) Center_Manager.Instance.authManager.AddCurrency(reward);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong!");
            speaker.GetComponent<NPC>().player.GetComponent<PlayerMovement>().canMove = true;
            speaker.GetComponent<NPC>().player.GetComponent<PlayerCombat>().canAttack = true;
            speaker.GetComponent<NPC>().responseText.text = "Wrong!";
            speaker.GetComponent<NPC>().player.GetComponent<PlayerCombat>().TakeDamage(1);
            gameObject.SetActive(false);
        }
    }
}
