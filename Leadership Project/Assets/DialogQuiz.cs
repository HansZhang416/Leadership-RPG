using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public string answer;
    [TextArea(3, 10)]
    public List<string> answers;

    [Header("Quiz UI")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI rewardText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
