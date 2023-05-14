using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeSpeaking : MonoBehaviour
{
    public List<GameObject> conversations;
    public int selectedConvo = 0;
    public int currentLine = 0;
    public int audienceSentiment = 0;
    
    void PickConversation() {
        selectedConvo = Random.Range(0,conversations.Count - 1);
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
        PickConversation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
